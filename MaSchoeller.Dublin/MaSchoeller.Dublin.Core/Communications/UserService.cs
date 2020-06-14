using BCrypt.Net;
using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Database;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using NHibernate.Dialect.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _users;
        private readonly ILogger<UserService>? _logger;

        public UserService(ISecurityHelper connectionHelper,
                           IUserRepository userRepository,
                           ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _users = userRepository;
            _logger = logger;
        }

        public PasswordChangeResult ChangePassword(string oldPassword, string newPassword)
        {
            if (!Validate())
                return new PasswordChangeResult { Reason = OperationResult.NotAuthenticated };

            var success = SecurityHelper.UpdatePassword(User!.Username, oldPassword, newPassword);
            return new PasswordChangeResult { Reason = success ? OperationResult.Success : OperationResult.SaveFailure };
        }

        public LoginResult Login(string username, string password)
        {
            var (success, user) = SecurityHelper.Login(username, password);
            if (success)
            {
                return new LoginResult
                {
                    Reason = OperationResult.Success,
                    Token = SecurityHelper.CreateToken(user!)
                };
            }
            else
            {
                return new LoginResult
                {
                    Reason = OperationResult.SaveFailure,
                    Token = null,
                };
            }

        }

        public IEnumerable<User> GetAllUsers()
        {
            if (!Validate())
            {
                return Enumerable.Empty<User>();
            }

            return _users.GetAll();
        }

        public AdminResult SetAdminRights(int userId, bool AdminRight)
        {
            if (!Validate())
                return new AdminResult { Reason = OperationResult.NotAuthenticated };

            if (!User!.IsAdmin)
                return new AdminResult { Reason = OperationResult.NotAuthorized };

            var result = _users.Update(userId, u => u.IsAdmin = AdminRight);
            return new AdminResult { Reason = result };
        }

        public SaveOrUpdateResult SaveOrUpdateUser(User user)
        {
            if (!Validate())
                return new SaveOrUpdateResult { Reason = OperationResult.NotAuthenticated };

            if (!User!.IsAdmin)
                return new SaveOrUpdateResult { Reason = OperationResult.NotAuthorized };

            OperationResult reason;
            //Note: if the Id is zero or negative, the id is not set and it's a create user action
            //      in Production i would use a DTO to handle data and maybe send the id as separate parameter of type int? to indicate my intention.
            //      But for this it should work.
            if (user.Id <= 0)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("geheim");
                reason = _users.Save(user);
            }
            else
            {
                reason = _users.Update(user);
            }
            return new SaveOrUpdateResult { Reason = reason };
        }

      

        public DeleteResult DeleteUser(User user)
        {
            if (!Validate())
                return new DeleteResult() { Reason = OperationResult.NotAuthenticated };

            if (!User!.IsAdmin)
                return new DeleteResult() { Reason = OperationResult.NotAuthorized };

            var result = _users.Delete(user);
            return new DeleteResult() { Reason = result };
        }
    }
}
