using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using System;
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
            CleanupViewModel();

            var window = new AddLeasingWindow
            {
                DataContext = _viewModel,
                Owner = Application.Current.MainWindow
            };
            
            Task.Run(() =>
            {
                _blocker.WaitOne();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (window.DialogResult is null)
                        window.DialogResult = true;
                });
            });
            window.ShowDialog();
        }


        private void CleanupViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
