using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class CalculationUnitViewModel : ViewModelBase
    {

        private List<BusinessUnitMonthCost> _costs = new List<BusinessUnitMonthCost>();

        public List<BusinessUnitMonthCost> Costs
        {
            get => _costs;
            set => SetProperty(ref _costs, value);
        }
    }
}
