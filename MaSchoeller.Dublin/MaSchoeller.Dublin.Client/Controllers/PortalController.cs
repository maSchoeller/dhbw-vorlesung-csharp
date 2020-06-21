using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class PortalController : ControllerBase
    {
        private readonly PortalViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ChangePasswordDialogController _passwordController;

        public PortalController(PortalViewModel viewModel, 
                                ClientConnectionHandler connectionHandler,
                                ChangePasswordDialogController passwordController)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _passwordController = passwordController;
        }

        public override object Initialize()
        {
            _viewModel.Name = _connectionHandler.ActiveUser!.Fullname;
            _viewModel.Username = _connectionHandler.ActiveUser!.Username;
            _viewModel.ChangePassswordCommand = 
                ConfigurableCommand.Create(ChangePasswordExecute).Build();
            return _viewModel;
        }

        public void ChangePasswordExecute(object o)
        {
            _passwordController.ShowDialog();
        }
    }
}
