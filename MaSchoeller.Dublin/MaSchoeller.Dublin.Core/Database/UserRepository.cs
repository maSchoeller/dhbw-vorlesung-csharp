﻿using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConnectionFactory factory, ILogger<UserRepository>? logger = null)
            : base(factory, logger) { }

        public User? FindByName(string username)
        {
            using var session = Factory.OpenSession();
            return session.Query<User>()
                          .FirstOrDefault(u => u.Username.ToUpper() == username.ToUpper());
        }

        public override OperationResult Update(User entity)
        {
            var user = FindById(entity.Id);
            if (user is null) return OperationResult.NotFound;
            entity.PasswordHash = user.PasswordHash;
            return base.Update(entity);
        }

        public override OperationResult Delete(User entity)
        {
            var user = FindById(entity.Id);
            if (user is null) return OperationResult.NotFound;
            entity.PasswordHash = user.PasswordHash;
            return base.Delete(entity);
        }

        public override OperationResult Save(User entity)
        {
            using (var session = Factory.OpenSession())
            {
                var exists = session.Query<User>().Any(u => u.Username.ToUpper() == entity.Username.ToUpper());
                if (exists) return OperationResult.AlreadyExists;
            }
            return base.Save(entity);
        }
    }
}
