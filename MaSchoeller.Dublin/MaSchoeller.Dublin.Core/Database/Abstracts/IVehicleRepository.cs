using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database.Abstracts
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        (OperationResult result, Tour? tour) SaveTour(Tour tour);
        (OperationResult result, Tour? tour) DeleteTour(Tour tour);

        IEnumerable<Tour> GetToursByVehicle(int id);

        IEnumerable<VehicleMonthCost> GetAllVehicleMonthCosts();
    }
}
