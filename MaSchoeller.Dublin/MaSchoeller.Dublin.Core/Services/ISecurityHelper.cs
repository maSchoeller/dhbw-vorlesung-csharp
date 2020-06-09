using MaSchoeller.Dublin.Core.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    public interface ISecurityHelper
    {
        //Note: In a real world application, you should separate the security helper in two services.
        //      One for the tokens itself and one for the authentication, in this small project i think it's ok.
        //      There is no need for over engineering.

        string CreateToken(string username, bool isAdmin = false);
        bool ValidateToken(string token);
        UserContext? GetUserContext(string token);
        bool Login(string username, string password);
        bool UpdatePassword(string username, string oldPassword, string newPassword);
    }
}
