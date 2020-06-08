using MaSchoeller.Dublin.Core.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    internal abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected IConnectionFactory Factory { get; }
        protected ILogger? Logger { get; }

        protected BaseRepository(IConnectionFactory factory) 
            : this(factory, null) { }

        protected BaseRepository(IConnectionFactory factory, ILogger? logger)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Logger = logger;
        }

        public IEnumerable<T> GetAll()
        {
            using var session = Factory.OpenSession();
            return session.CreateCriteria<T>().List<T>();
        }

        public void Delete(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Delete(entity);
            transaction.Commit();
        }

        public void Save(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Save(entity);
            transaction.Commit();
        }

        public T FindById(int id)
        {
            using var session = Factory.OpenSession();
            return session.Get<T>(id);
        }
    }
}
