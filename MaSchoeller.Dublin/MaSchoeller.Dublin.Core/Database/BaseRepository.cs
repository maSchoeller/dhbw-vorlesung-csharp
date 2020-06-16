using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using NHibernate;
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

        public virtual (OperationResult result, T entity) Delete(T entity)
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
                return (OperationResult.CascadingDeleteError, entity);
            }
            return (OperationResult.Success, entity);

        }

        public virtual (OperationResult result, T entity) Save(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();

            try
            {
                var id = session.Save(entity);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure, entity);
            }
            return (OperationResult.Success, entity);

        }

        public virtual (OperationResult result, T entity) Update(T entity)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            { 
                //Note: uncomment for checking version conflicts
                //dynamic t = entity;
                //t.Version = 1;
                session.Update(entity);
                transaction.Commit();
            }
            catch(StaleObjectStateException e)
            {
                return (OperationResult.SaveConflict, entity);
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure, entity);
            }
            return (OperationResult.Success, entity);

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

        public virtual (OperationResult result, T entity) Update(int id, Action<T> modifier)
        {
            using var session = Factory.OpenSession();
            var model = session.Get<T>(id);
            if (model is null) return (OperationResult.NotFound, null!);
            modifier(model);
            using var transaction = session.BeginTransaction();
            try
            {
                session.Update(model);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure, model);
            }
            return (OperationResult.Success, model);
        }
    }
}
