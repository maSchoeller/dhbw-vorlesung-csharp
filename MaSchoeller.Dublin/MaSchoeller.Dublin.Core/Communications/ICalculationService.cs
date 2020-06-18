using MaSchoeller.Dublin.Core.Communications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceContract()]
    public interface ICalculationService
    {
        [OperationContract]
        IEnumerable<CalculationMonthSet> GetCalculationMonthSets();

        [OperationContract]
        IEnumerable<CalculationsBusinessUnitSet> GetCalculationsBusinessUnitSets();

    }
}
