using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    internal class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService>? _logger;

        public UserService(ISecurityHelper connectionHelper,
                           ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _logger = logger;
        }

        public PasswordChangeResult ChangePassword(string oldPassword, string newPassword)
        {
            if (!Validate())
                return new PasswordChangeResult
                {
                    Success = false,
                    ErrorMessage = "Ein Fehler ist aufgetreten, das Passwort wurde nicht aktualisiert."
                };

            var success = SecurityHelper.UpdatePassword(User.Username, oldPassword, newPassword);
            if (success)
                return new PasswordChangeResult { Success = true };
            else
                return new PasswordChangeResult
                {
                    Success = false,
                    ErrorMessage = "Ein Fehler ist aufgetreten, das Passwort wurde nicht aktualisiert."
                };
        }

        public LoginResult Login(string username, string password)
        {
            if (SecurityHelper.Login(username, password))
            {
                return new LoginResult
                {
                    Success = true,
                    Token = SecurityHelper.CreateToken(username),
                    IsAdmin = false
                };
            }
            else
            {
                return new LoginResult
                {
                    Success = false,
                    Token = null,
                    ErrorMessage = "Benutzername oder Password sind falsch!"
                };
            }

        }
    }
}
