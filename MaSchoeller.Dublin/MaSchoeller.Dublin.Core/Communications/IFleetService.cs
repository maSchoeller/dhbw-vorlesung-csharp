using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceContract()]
    public interface IFleetService
    {

        [OperationContract]
        DeleteResult DeleteBuisnessUnit(BuisnessUnit buisnessUnit);

        [OperationContract]
        SaveOrUpdateResult SaveOrUpdateBuisnessUnit(BuisnessUnit buisnessUnit);

        [OperationContract]
        IEnumerable<BuisnessUnit> GetAllBuisnessUnits();
    }
}
