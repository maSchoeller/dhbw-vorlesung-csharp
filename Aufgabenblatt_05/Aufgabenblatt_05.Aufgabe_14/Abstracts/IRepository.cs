using System.Collections.Generic;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IRepository<T> where T : class
    {
        void Delete(T entity);
        IList<T> GetAll();
        void Save(T entity);
    }
}