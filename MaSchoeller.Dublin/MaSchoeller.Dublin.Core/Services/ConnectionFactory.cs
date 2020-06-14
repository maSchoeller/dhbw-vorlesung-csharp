using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Database;
using NHibernate;
using System;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class ConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly ServerConfiguration _configuration;
        private readonly ISessionFactory _factory;

        public ConnectionFactory(ServerConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var sqliteConf = SQLiteConfiguration.Standard.UsingFile(_configuration.DatabasePath);
            _factory = Fluently.Configure()
                               .Database(sqliteConf)
                               .Mappings(m => AddMappings(m.FluentMappings))
                               .BuildSessionFactory();
        }

        private static void AddMappings(FluentMappingsContainer mappings)
        {
            mappings.Add<BuisnessUnitMapping>();
            mappings.Add<VehicleMapping>();
            mappings.Add<UserMapping>();
            mappings.Add<EmployeeMapping>();
            mappings.Add<VehicleEmployeeMapping>();

            mappings.Conventions.Add(DefaultLazy.Never());
        }

        public ISession OpenSession() => _factory.OpenSession();
        public void Dispose() => _factory.Dispose();
    }
}
