using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Abstracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Delete(T entity);
        void Save(T entity);
        T FindById(int id);

    }
}
