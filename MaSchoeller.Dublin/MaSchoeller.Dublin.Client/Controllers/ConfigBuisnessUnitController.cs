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
using System.Windows;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigBusinessUnitController : ControllerBase
    {
        private readonly ConfigBusinessUnitViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;

        public ConfigBusinessUnitController(ConfigBusinessUnitViewModel viewModel,
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


            _viewModel.SaveCommand = ConfigurableCommand.Create(ExecuteSaveCommand, o => _viewModel.BuisnessUnits.Any(u => u.EditState == EditState.Modified))
                                                        .ObserveCommandManager()
                                                        .Build();

            _viewModel.DeleteCommand = ConfigurableCommand.Create(ExecuteDeleteCommand, o => !(_viewModel.SelectedBuisnessUnit is null))
                                                       .Observe(_viewModel, () => _viewModel.SelectedBuisnessUnit)
                                                       .Build();

            return _viewModel;
        }



        private void ExecuteNewCommand(object o)
        {
            _viewModel.SelectedBuisnessUnit = new DisplayBusinessUnit();
            _viewModel.BuisnessUnits.Add(_viewModel.SelectedBuisnessUnit);
        }



        private async void ExecuteDeleteCommand(object o)
        {
            if (!(_viewModel.SelectedBuisnessUnit is null))
            {
                var userClient = _connectionHandler.FleetsClient;
                await _lostHelper.InvokeAsync(async () =>
                {
                    var result = await userClient.DeleteBusinessUnitAsync(_viewModel.SelectedBuisnessUnit!.AsBusinessUnit());
                    if (result.Reason == OperationResult.Success)
                    {
                        _viewModel.BuisnessUnits.Remove(_viewModel.SelectedBuisnessUnit);
                        _viewModel.SelectedBuisnessUnit = null;
                    }
                    else
                    {
                        _viewModel.SelectedBuisnessUnit!.ErrorMessage = DisplayMessages.UserCantDelete;
                    }
                });
            }
        }

        private async void ExecuteSaveCommand(object o)
        {
            var client = _connectionHandler.FleetsClient;
            foreach (var businessUnit in _viewModel.BuisnessUnits)
            {
                if (businessUnit.EditState == EditState.Modified)
                {
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var result = await client.SaveOrUpdateBusinessUnitAsync(businessUnit.AsBusinessUnit());
                        switch (result.Reason)
                        {
                            case OperationResult.Success:
                            {
                                businessUnit.EditState = EditState.None;
                                businessUnit.Version = result.BuisnessUnit.Version;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                businessUnit.EditState = EditState.InValid;
                                businessUnit.ErrorMessage = DisplayMessages.UserIdAlreadyExists;

                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                businessUnit.EditState = EditState.InValid;
                                businessUnit.ErrorMessage = DisplayMessages.ConcurrentServerException;

                            }
                            break;
                            default:
                            {
                                businessUnit.EditState = EditState.InValid;
                                businessUnit.ErrorMessage = DisplayMessages.ErrorOnSave;
                            }
                            break;
                        }
                    });
                }
            }
        }

        public override async Task EnterAsync()
        {
            var fleetclient = _connectionHandler.FleetsClient;
            await _lostHelper.InvokeAsync(async () =>
            {
                var result = await fleetclient.GetAllBusinessUnitsAsync();
                _viewModel.BuisnessUnits = new ObservableCollection<DisplayBusinessUnit>(result.Select(u => new DisplayBusinessUnit(u)));
            });
        }
    }
}
