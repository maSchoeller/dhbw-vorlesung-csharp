using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Abstracts
{
    public interface IConnectionFactory : IDisposable
    {
        ISession OpenSession();
    }
}
