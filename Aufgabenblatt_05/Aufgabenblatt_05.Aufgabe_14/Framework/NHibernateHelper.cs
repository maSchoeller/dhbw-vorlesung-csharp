using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.Framework
{
    public class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;

        public static readonly string DatabaseFile = @"Database\movies.db"; 
        // Es macht null Sinn den Connectionstring über das Repository mit zugeben, 
        // nach dem ersten Instanzieren wird dieser nämlich ignoriert...
        // Was zu schwer wartbaren Sideeffekten führt...

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
