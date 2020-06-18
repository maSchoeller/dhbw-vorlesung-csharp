using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MaSchoeller.Dublin.Client.Proxies.Fleets
{
    public partial class BusinessUnit
    {
        //Note: not in all cases true, but for my purpose is works
        public override bool Equals(object obj) 
            => obj is BusinessUnit b && b.Id == Id;
    }
}
