using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Augabenblatt_05.Aufgabe_13.Framework
{
    public class Repository<T> where T : class
    {
        public Repository(string databaseFile)
        {
            NHibernateHelper.DatabaseFile = databaseFile;

        }

        public IList<T> GetAll()
        {
            using var session = NHibernateHelper.OpenSession();
            var returnList = session.CreateCriteria<T>().List<T>();
            return returnList;
        }

        public void Delete(T entity)
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Delete(entity);
            transaction.Commit();
        }

        public void Save(T entity)
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Save(entity);
            transaction.Commit();
        }
    }
}
