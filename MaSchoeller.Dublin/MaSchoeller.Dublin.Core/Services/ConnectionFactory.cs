using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using MaSchoeller.Dublin.Core.Abstracts;
using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Database;
using NHibernate;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class ConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly ServerConfiguration _configuration;
        private readonly ISessionFactory _factory;

        public ConnectionFactory(ServerConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var sqliteConf = SQLiteConfiguration.Standard.UsingFile(_configuration.DatabasePath).ShowSql();
            _factory = Fluently.Configure().Database(sqliteConf)
                                     .Mappings(m => AddMappings(m.FluentMappings))
                                     .BuildSessionFactory();
        }

        private void AddMappings(FluentMappingsContainer mappings)
        {
            mappings.Add<BuisnessUnitMapping>();
            mappings.Add<UserMapping>();
            mappings.Add<EmployeeMapping>();


            mappings.Conventions.Add(DefaultLazy.Never());
        }

        public ISession OpenSession() => _factory.OpenSession();
        public void Dispose() => _factory.Dispose();
    }
}
