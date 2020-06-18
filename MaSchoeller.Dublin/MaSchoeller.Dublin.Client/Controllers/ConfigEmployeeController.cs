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
    public class ConfigEmployeeController : ControllerBase
    {
        private readonly ConfigEmployeeViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;

        public ConfigEmployeeController(ConfigEmployeeViewModel viewModel, 
                                        ClientConnectionHandler connectionHandler,
                                        ConnectionLostHelper lostHelper)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
        }

        public override object Initialize()
        {
            _viewModel.NewCommand = ConfigurableCommand.Create(ExecuteNewCommand).Build();

            _viewModel.SaveCommand = ConfigurableCommand.Create(ExecuteSaveCommand, o => _viewModel.Employees.Any(u => u.EditState == EditState.Modified))
                                                        .ObserveCommandManager()
                                                        .Build();

            _viewModel.DeleteCommand = ConfigurableCommand.Create(ExecuteDeleteCommand, o => !(_viewModel.SelectedEmployee is null))
                                                       .Observe(_viewModel, () => _viewModel.SelectedEmployee)
                                                       .Build();

            return _viewModel;
        }

        public override async Task EnterAsync()
        {
            var fleetclient = _connectionHandler.FleetsClient;
            var emp = await fleetclient.GetAllEmployeesAsync();
            _viewModel.Employees = new ObservableCollection<DisplayEmployee>(emp.Select(e => new DisplayEmployee(e)));
            _viewModel.BusinessUnits = await fleetclient.GetAllBusinessUnitsAsync();
        }

        private void ExecuteNewCommand(object o)
        {
            var emp = new DisplayEmployee();
            _viewModel.Employees.Add(emp);
            _viewModel.SelectedEmployee = emp;
        }

        private async void ExecuteSaveCommand(object o)
        {
            var client = _connectionHandler.FleetsClient;
            foreach (var employee in _viewModel.Employees)
            {
                if (employee.EditState == EditState.Modified)
                {
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var result = await client.SaveOrUpdateEmployeeAsync(employee.AsEmployee());
                        switch (result.Reason)
                        {
                            case OperationResult.Success:
                            {
                                employee.EditState = EditState.None;
                                employee.Version = result.Employee.Version;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMesages.EmployeeIdAlreadyExists;

                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMesages.ConcurrentServerException;

                            }
                            break;
                            default:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMesages.ErrorOnSave;

                            }
                            break;
                        }
                    });
                }
            }
        }

        private async void ExecuteDeleteCommand(object o)
        {
            if (!(_viewModel.SelectedEmployee is null))
            {
                var userClient = _connectionHandler.FleetsClient;
                await _lostHelper.InvokeAsync(async () =>
                {
                    var result = await userClient.DeleteEmployeeAsync(_viewModel.SelectedEmployee.AsEmployee());
                    if (result.Reason == OperationResult.Success)
                    {
                        _viewModel.Employees.Remove(_viewModel.SelectedEmployee);
                        _viewModel.SelectedEmployee = null;
                    }
                    else
                    {
                        _viewModel.SelectedEmployee.ErrorMessage = DisplayMesages.EmployeeCantDelete;
                    }
                });
            }
        }
    }
}
