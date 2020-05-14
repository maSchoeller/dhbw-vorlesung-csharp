using Aufgabenblatt_05.Aufgabe_13.Framework;
using Aufgabenblatt_05.Aufgabe_13.Models;
using Aufgabenblatt_05.Aufgabe_13.ViewModels;
using Aufgabenblatt_05.Aufgabe_13.Views;

namespace Aufgabenblatt_05.Aufgabe_13.Controllers
{
    public class WindowAddController
    {
        private readonly AddWindow _view;
        private readonly WindowAddViewModel _viewModel;

        public WindowAddController()
        {
            _viewModel = new WindowAddViewModel()
            {
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            _view = new AddWindow()
            {
                DataContext = _viewModel
            };
        }

        private void ExecuteOkCommand(object _)
        {
            _view.DialogResult = true;
            _view.Close();
        }

        private void ExecuteCancelCommand(object _)
        {
            _view.DialogResult = false;
            _view.Close();
        }

        public Customer? AddEmployee()
        {
            _viewModel.Model = new Customer();
            var result = _view.ShowDialog() ?? false;
            return result ? _viewModel.Model : null;
        }
    }
}
