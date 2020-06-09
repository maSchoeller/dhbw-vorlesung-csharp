using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database.Abstracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        (IQueryable<T> query, IDisposable session) GetQuery();
        void Delete(T entity);
        void Update(T entity);
        void Save(T entity);
        T? FindById(int id);

    }
}
