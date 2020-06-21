using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigBusinessUnitController : ControllerBase
    {
        private readonly ConfigBusinessUnitViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly ILogger<ConfigBusinessUnitController>? _logger;

        public ConfigBusinessUnitController(ConfigBusinessUnitViewModel viewModel,
                                            ClientConnectionHandler connectionHandler,
                                            ConnectionLostHelper lostHelper,
                                            ILogger<ConfigBusinessUnitController>? logger = null)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
            _logger = logger;
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
            _viewModel.IsBusy = true;
            if (!(_viewModel.SelectedBuisnessUnit is null))
            {
                var delete = true;
                if (_viewModel.SelectedBuisnessUnit.IsSynced)
                {
                    var userClient = _connectionHandler.FleetsClient;
                    await _lostHelper.InvokeAsync(async () =>
                    {
                        var result = await userClient.DeleteBusinessUnitAsync(_viewModel.SelectedBuisnessUnit!.AsBusinessUnit());
                        if (result.Reason != OperationResult.Success)
                        {
                            _viewModel.SelectedBuisnessUnit!.ErrorMessage = DisplayMessages.CantDelete;
                            delete = false;
                        }
                    });
                }
                if (delete)
                {
                    _viewModel.BuisnessUnits.Remove(_viewModel.SelectedBuisnessUnit);
                    _viewModel.SelectedBuisnessUnit = null;
                }

            }
            _viewModel.IsBusy = false;
        }

        private async void ExecuteSaveCommand(object o)
        {
            _viewModel.IsBusy = true;
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
                                businessUnit.IsSynced = true;
                                businessUnit.Version = result.BuisnessUnit.Version;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                businessUnit.EditState = EditState.InValid;
                                businessUnit.ErrorMessage = DisplayMessages.ModelAlreadyExists;

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
            _viewModel.IsBusy = false;
        }

        public override async Task EnterAsync()
        {
            _viewModel.IsBusy = true;
            _viewModel.SelectedBuisnessUnit = null;
            _viewModel.BuisnessUnits = new ObservableCollection<DisplayBusinessUnit>();
            try
            {
                var fleetclient = _connectionHandler.FleetsClient;
                var result = await fleetclient.GetAllBusinessUnitsAsync();
                _viewModel.BuisnessUnits = new ObservableCollection<DisplayBusinessUnit>(result.Select(u => new DisplayBusinessUnit(u)));
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e,"");
                _lostHelper.ShowConnectionLost();
            }
            _viewModel.IsBusy = false;
        }
    }
}
