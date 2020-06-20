using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class CalculationMonthViewModel : ViewModelBase
    {
        private List<VehicleMonthCost> _costs = new List<VehicleMonthCost>();
        public List<VehicleMonthCost> Costs { get => _costs; set => SetProperty(ref _costs, value); }
    }
}
