using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IConnectionFactory factory, ILogger<VehicleRepository>? logger = null) 
            : base(factory,logger) { }
    }
}
