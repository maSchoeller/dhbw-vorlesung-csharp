using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_04
{
    public class ObjectFactory
    {
        public TObject CreateInstance<TObject>() where TObject : class, new() 
            => new TObject();
    }
}
