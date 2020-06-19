using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigVehicleController : ControllerBase
    {
        private readonly ConfigVehicleViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;

        public ConfigVehicleController(ConfigVehicleViewModel viewModel, 
                                       ClientConnectionHandler clientConnectionHandler,
                                       ConnectionLostHelper lostHelper)
        {
            _viewModel = viewModel;
            _connectionHandler = clientConnectionHandler;
            _lostHelper = lostHelper;
        }

        public override object Initialize()
        {
            _viewModel.NewCommand = ConfigurableCommand.Create(ExecuteNewCommand).Build();
            _viewModel.SaveCommand = ConfigurableCommand.Create(ExecuteSaveCommand, o => _viewModel.Vehicles.Any(u => u.EditState == EditState.Modified))
                                                        .ObserveCommandManager()
                                                        .Build();

            _viewModel.DeleteCommand = ConfigurableCommand.Create(ExecuteDeleteCommand, o => !(_viewModel.SelectedVehicle is null))
                                                       .Observe(_viewModel, () => _viewModel.SelectedVehicle)
                                                       .Build();

            _viewModel.AddTourCommand = ConfigurableCommand.Create(ExecuteAddTourCommand).Build();
            _viewModel.RemoveTourCommand = ConfigurableCommand.Create(ExecuteRemoveTourCommand).Build();

            _viewModel.TabChanged += ChangeTabView;
            return _viewModel;
        }

        private void ChangeTabView(object sender, EventArgs e)
        {

        }

        public override async Task EnterAsync()
        {
            var vehicles = await _connectionHandler.FleetsClient.GetAllVehiclesAsync();
            _viewModel.Vehicles = new ObservableCollection<DisplayVehicle>(vehicles.Select(v => new DisplayVehicle(v)));
            _viewModel.SelectedVehicle = null;
        }

        public void ExecuteNewCommand(object o)
        {
            var model = new DisplayVehicle();
            _viewModel.Vehicles.Add(model);
            _viewModel.SelectedVehicle = model;
        }

        public async void ExecuteSaveCommand(object o)
        {
            var client = _connectionHandler.FleetsClient;
            foreach (var vehicle in _viewModel.Vehicles)
            {
                if (vehicle.EditState == EditState.Modified)
                {
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var validVehicle = vehicle.AsVehicle();
                        if (validVehicle is null)
                        {
                            vehicle.ErrorMessage = DisplayMessages.VehicleIsNotValid;
                            return;
                        }
                        var result = await client.SaveOrUpdateVehicleAsync(validVehicle);
                        switch (result.Reason)
                        {
                            case OperationResult.Success:
                            {
                                vehicle.EditState = EditState.None;
                                vehicle.Id = result.Vehicle.Id;
                                vehicle.Version = result.Vehicle.Version;
                                vehicle.IsSynced = true;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                vehicle.EditState = EditState.InValid;
                                vehicle.ErrorMessage = DisplayMessages.EmployeeIdAlreadyExists;
                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                vehicle.EditState = EditState.InValid;
                                vehicle.ErrorMessage = DisplayMessages.ConcurrentServerException;
                            }
                            break;
                            default:
                            {
                                vehicle.EditState = EditState.InValid;
                                vehicle.ErrorMessage = DisplayMessages.ErrorOnSave;
                            }
                            break;
                        }
                    });
                }
            }
        }

        public async void ExecuteDeleteCommand(object o)
        {
            if (!(_viewModel.SelectedVehicle is null))
            {
                var delete = true;
                if (_viewModel.SelectedVehicle.IsSynced)
                {
                    var userClient = _connectionHandler.FleetsClient;
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var result = await userClient.DeleteVehicleAsync(_viewModel.SelectedVehicle!.AsVehicle());
                        if (result.Reason != OperationResult.Success)
                        {
                            _viewModel.SelectedVehicle!.ErrorMessage = DisplayMessages.UserCantDelete;
                            delete = false;
                        }
                    });
                }
                if (delete)
                {
                    _viewModel.Vehicles.Remove(_viewModel.SelectedVehicle);
                    _viewModel.SelectedVehicle = null;
                }
                
            }
        }

        public void ExecuteAddTourCommand(object o)
        {

        }

        public void ExecuteRemoveTourCommand(object o)
        {

        }
    }
}
