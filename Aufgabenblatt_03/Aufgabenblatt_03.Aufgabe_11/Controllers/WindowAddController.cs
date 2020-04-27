using Aufgabenblatt_03.Aufgabe_11.Framework;
using Aufgabenblatt_03.Aufgabe_11.Models;
using Aufgabenblatt_03.Aufgabe_11.ViewModels;
using Aufgabenblatt_03.Aufgabe_11.Views;

namespace Aufgabenblatt_03.Aufgabe_11.Controllers
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
            _view!.DialogResult = true;
            _view.Close();
        }

        private void ExecuteCancelCommand(object _)
        {
            _view!.DialogResult = false;
            _view.Close();
        }

        public Employee? AddEmployee()
        {
            _viewModel.Model = new Employee();
            var result = _view.ShowDialog() ?? false;
            return result ? _viewModel.Model : null;
        }
    }
}
