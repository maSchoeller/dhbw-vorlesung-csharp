﻿using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigEmployeeController : ControllerBase
    {
        private readonly ConfigEmployeeViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly ILogger<ConfigEmployeeController>? _logger;

        public ConfigEmployeeController(ConfigEmployeeViewModel viewModel,
                                        ClientConnectionHandler connectionHandler,
                                        ConnectionLostHelper lostHelper,
                                        ILogger<ConfigEmployeeController>? logger = null)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
            _logger = logger;
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
            _viewModel.IsBusy = true;
            _viewModel.Employees = new ObservableCollection<DisplayEmployee>();
            _viewModel.SelectedEmployee = null;
            _viewModel.BusinessUnits = new List<BusinessUnit>();
            _viewModel.SelectedEmployee = null;
            try
            {
                var fleetclient = _connectionHandler.FleetsClient;
                var emp = await fleetclient.GetAllEmployeesAsync();
                _viewModel.Employees = new ObservableCollection<DisplayEmployee>(emp.Select(e => new DisplayEmployee(e)));
                _viewModel.BusinessUnits = await fleetclient.GetAllBusinessUnitsAsync();
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e, "");
                _lostHelper.ShowConnectionLost();
            }
            _viewModel.IsBusy = false;
        }


        private void ExecuteNewCommand(object o)
        {
            var emp = new DisplayEmployee();
            _viewModel.Employees.Add(emp);
            _viewModel.SelectedEmployee = emp;
        }

        private async void ExecuteSaveCommand(object o)
        {
            _viewModel.IsBusy = true;
            var client = _connectionHandler.FleetsClient;
            foreach (var employee in _viewModel.Employees)
            {
                if (employee.EditState == EditState.Modified)
                {
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var validEmployee = employee.AsEmployee();
                        if (validEmployee is null)
                        {
                            employee.ErrorMessage = DisplayMessages.EmployeeIsNotValid;
                            return;
                        }
                        var result = await client.SaveOrUpdateEmployeeAsync(validEmployee);
                        switch (result.Reason)
                        {
                            case OperationResult.Success:
                            {
                                employee.EditState = EditState.None;
                                employee.Version = result.Employee.Version;
                                employee.IsSynced = true;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMessages.EmployeeIdAlreadyExists;

                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMessages.ConcurrentServerException;

                            }
                            break;
                            default:
                            {
                                employee.EditState = EditState.InValid;
                                employee.ErrorMessage = DisplayMessages.ErrorOnSave;

                            }
                            break;
                        }
                    });
                }
            }
            _viewModel.IsBusy = false;
        }

        private async void ExecuteDeleteCommand(object o)
        {
            _viewModel.IsBusy = true;
            if (!(_viewModel.SelectedEmployee is null))
            {
                bool delete = true;
                if (_viewModel.SelectedEmployee.IsSynced)
                {
                    var userClient = _connectionHandler.FleetsClient;
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var result = await userClient.DeleteEmployeeAsync(_viewModel.SelectedEmployee.AsEmployee());
                        if (result.Reason != OperationResult.Success)
                        {
                            _viewModel.SelectedEmployee.ErrorMessage = DisplayMessages.EmployeeCantDelete;
                            delete = false;
                        }
                    });
                }

                if (delete)
                {
                    _viewModel.Employees.Remove(_viewModel.SelectedEmployee);
                    _viewModel.SelectedEmployee = null;
                }
            }
            _viewModel.IsBusy = false;
        }
    }
}
