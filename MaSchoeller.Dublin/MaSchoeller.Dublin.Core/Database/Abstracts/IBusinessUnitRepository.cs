using MaSchoeller.Dublin.Core.Models;
using System.Collections.Generic;

namespace MaSchoeller.Dublin.Core.Database.Abstracts
{
    public interface IBusinessUnitRepository : IRepository<BusinessUnit>
    {

        IEnumerable<BusinessUnitMonthCost> GetAllCosts();
    }
}
