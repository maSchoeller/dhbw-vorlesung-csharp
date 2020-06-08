using MaSchoeller.Dublin.Core.Communications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Abstracts
{
    [ServiceContract()]
    public interface IFleetService
    {
        [OperationContract]
        string GetTestData();

    }
}
