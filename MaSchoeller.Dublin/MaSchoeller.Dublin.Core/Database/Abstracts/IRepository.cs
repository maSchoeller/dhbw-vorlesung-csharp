using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database.Abstracts
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetAll();
        (IQueryable<T> query, IDisposable session) GetQuery();
        OperationResult Delete(T entity);

        OperationResult Update(int id, Action<T> modifier);

        OperationResult Update(T entity);
        OperationResult Save(T entity);
        T? FindById(int id);

    }
}
