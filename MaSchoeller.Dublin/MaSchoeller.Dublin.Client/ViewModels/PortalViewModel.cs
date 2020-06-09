using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class PortalViewModel : ViewModelBase
    {

        public ICommand ChangePassswordCommand { get; set; } = null!;
    }
}
