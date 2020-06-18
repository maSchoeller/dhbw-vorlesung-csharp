using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AddLeasingDialogController
    {
        private readonly AddLeasingViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly AutoResetEvent _blocker = new AutoResetEvent(false);

        public AddLeasingDialogController(AddLeasingViewModel viewModel,
                                          ClientConnectionHandler connectionHandler)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
        }


        public void ShowDialog()
        {
            var window = new AddLeasingWindow();
            Task.Run(() => { _blocker.WaitOne(); Application.Current.Dispatcher.Invoke(() => window.DialogResult = true); });
        }
    }
}
