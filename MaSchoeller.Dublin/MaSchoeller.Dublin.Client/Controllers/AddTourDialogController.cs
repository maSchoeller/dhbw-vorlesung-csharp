using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AddTourDialogController
    {
        private readonly AddTourViewModel _viewModel;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ILogger<AddTourDialogController>? _logger;
        private readonly AutoResetEvent _blocker = new AutoResetEvent(false);

        private Tour? _tour;
        private int _vehicleId;

        public AddTourDialogController(AddTourViewModel viewModel,
                                       ConnectionLostHelper lostHelper,
                                       ClientConnectionHandler connectionHandler,
                                       ILogger<AddTourDialogController>? logger = null)
        {
            _viewModel = viewModel;
            _lostHelper = lostHelper;
            _connectionHandler = connectionHandler;
            _logger = logger;
            _viewModel.AddCommand = ConfigurableCommand.Create(ExecuteAddCommand, o => !(_viewModel.SelectedEmployee is null || _viewModel.StartDate is null))
                                                       .Observe(_viewModel, () => _viewModel.StartDate)
                                                       .Observe(_viewModel, () => _viewModel.SelectedEmployee)
                                                       .Build();
            _viewModel.AbordCommand = ConfigurableCommand.Create(ExecuteAbordCommand).Build();
        }

        private void ExecuteAbordCommand(object obj)
        {
            _blocker.Set();   
        }

        private async  void ExecuteAddCommand(object obj)
        {
            try
            {
                var client = _connectionHandler.FleetsClient;
                var result = await client.SaveTourAsync(new Tour
                {
                    EmployeeId = _viewModel.SelectedEmployee!.Id,
                    VehicleId = _vehicleId,
                    Start = _viewModel.StartDate!.Value,
                    End = _viewModel.EndDate,
                });

                if (result.Reason != OperationResult.Success)
                {
                    MessageBox.Show(DisplayMessages.ErrorAddingTour, "Fehler", MessageBoxButton.OK);
                }
                else
                {
                    _tour = result.Tour;
                }

            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "");
                MessageBox.Show(DisplayMessages.ErrorAddingTour, "Fehler", MessageBoxButton.OK);
            }

            _blocker.Set();
        }

        public async Task<Tour?> ShowDialogAsync(DisplayVehicle displayVehicle)
        {
            _vehicleId = displayVehicle.Id;
            CleanupViewModel();
            try
            {
                var client = _connectionHandler.FleetsClient;
                var result = await client.GetPossibleEmployeesByVehicleAsync(displayVehicle.Id);
                _viewModel.Employees = new ObservableCollection<Proxies.Fleets.Employee>(result);
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e, "");
                MessageBox.Show(DisplayMessages.ErrorAddingTour,"Fehler",MessageBoxButton.OK);
                return null;
            }

            var window = new AddTourWindow
            {
                DataContext = _viewModel,
                Owner = Application.Current.MainWindow
            };
            
            _ = Task.Run(() =>
            {
                _blocker.WaitOne();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (window.DialogResult is null)
                        window.DialogResult = true;
                });
            });
            window.ShowDialog();
            return _tour;
        }


        private void CleanupViewModel()
        {
            _viewModel.SelectedEmployee = null;
            _viewModel.Employees.Clear();
            _viewModel.EndDate = null;
            _viewModel.StartDate = DateTime.Today;
        }
    }
}
