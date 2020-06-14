using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
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

        public virtual ICollection<T> GetAll()
        {
            using var session = Factory.OpenSession();
            return session.CreateCriteria<T>().List<T>();
        }

        public virtual OperationResult Delete(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                session.Delete(entity);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return OperationResult.CascadingDeleteError;
            }
            return OperationResult.Success;
        }

        public virtual OperationResult Save(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                session.Save(entity);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return OperationResult.SaveFailure;
            }
            return OperationResult.Success;
        }

        public virtual OperationResult Update(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                session.Update(entity);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return OperationResult.SaveFailure;
            }
            return OperationResult.Success;
        }

        public virtual T FindById(int id)
        {
            using var session = Factory.OpenSession();
            return session.Get<T>(id);
        }

        public virtual (IQueryable<T> query, IDisposable session) GetQuery()
        {
            var session = Factory.OpenSession();
            return (session.Query<T>(), session);
        }

        public virtual OperationResult Update(int id, Action<T> modifier)
        {
            using var session = Factory.OpenSession();
            var model = session.Get<T>(id);
            if (model is null) return OperationResult.NotFound;
            modifier(model);
            using var transaction = session.BeginTransaction();
            try
            {
                session.Update(model);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return OperationResult.SaveFailure;
            }
            return OperationResult.Success;
        }
    }
}
