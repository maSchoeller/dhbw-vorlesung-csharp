using MaSchoeller.Dublin.Core.Database.Abstracts;
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
    }
}
