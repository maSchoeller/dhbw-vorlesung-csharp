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
        (OperationResult result,T entity) Delete(T entity);

        (OperationResult result, T entity) Update(int id, Action<T> modifier);

        (OperationResult result, T entity) Update(T entity);
        (OperationResult result, T entity) Save(T entity);
        T? FindById(int id);

    }
}
