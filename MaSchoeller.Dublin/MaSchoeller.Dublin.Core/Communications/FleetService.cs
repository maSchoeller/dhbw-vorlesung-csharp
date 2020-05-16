using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    public class FleetService : IFleetService
    {
        public string GetTestData()
        {
            return "It works";
        }
    }
}
