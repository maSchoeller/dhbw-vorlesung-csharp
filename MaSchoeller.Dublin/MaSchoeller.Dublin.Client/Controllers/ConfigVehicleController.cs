using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigVehicleController : ControllerBase
    {
        private readonly ConfigVehicleViewModel _viewModel;

        public ConfigVehicleController(ConfigVehicleViewModel viewModel)
        {
            _viewModel = viewModel;
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
            return _viewModel;
        }

        public override async Task EnterAsync()
        {

        }

        public void ExecuteNewCommand(object o)
        {

        }

        public void ExecuteSaveCommand(object o)
        {

        }

        public void ExecuteDeleteCommand(object o)
        {

        }
    }
}
