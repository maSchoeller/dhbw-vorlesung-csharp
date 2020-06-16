using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceContract()]
    public interface IUserService
    {
        [OperationContract]
        PasswordChangeResult ChangePassword(string oldPassword, string newPassword);

        [OperationContract]
        LoginResult Login(string username, string password);

        [OperationContract]
        SaveOrUpdateUserResult SaveOrUpdateUser(User user);

        [OperationContract]
        DeleteUserResult DeleteUser(User user);

        [OperationContract]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        AdminResult SetAdminRights(int userId, bool AdminRight);
    }
}
