using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Augabenblatt_05.Aufgabe_13.Framework
{
    public static class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;

        public static string DatabaseFile { get; set; }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory is null)
                {
                    InitializeDatabaseFactory();
                }
                return _sessionFactory!;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static void InitializeDatabaseFactory()
        {
            _sessionFactory = Fluently.Configure()
                                     .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql())
                                     .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                                     .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                                     .BuildSessionFactory();
        }
    }
}
