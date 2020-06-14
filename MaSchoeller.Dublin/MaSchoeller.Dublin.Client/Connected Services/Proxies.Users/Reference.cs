﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaSchoeller.Dublin.Client.Proxies.Users
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MaSchoeller.Dublin.Client.Proxies.Users.LoginResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MaSchoeller.Dublin.Client.Proxies.Users.SaveOrUpdateResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MaSchoeller.Dublin.Client.Proxies.Users.DeleteResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MaSchoeller.Dublin.Client.Proxies.Users.AdminResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MaSchoeller.Dublin.Client.Proxies.Users.PasswordChangeResult))]
    public partial class BaseResult : object
    {
        
        private MaSchoeller.Dublin.Client.Proxies.Users.OperationResult ReasonField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MaSchoeller.Dublin.Client.Proxies.Users.OperationResult Reason
        {
            get
            {
                return this.ReasonField;
            }
            set
            {
                this.ReasonField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    public partial class LoginResult : MaSchoeller.Dublin.Client.Proxies.Users.BaseResult
    {
        
        private string TokenField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SaveOrUpdateResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    public partial class SaveOrUpdateResult : MaSchoeller.Dublin.Client.Proxies.Users.BaseResult
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DeleteResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    public partial class DeleteResult : MaSchoeller.Dublin.Client.Proxies.Users.BaseResult
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AdminResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    public partial class AdminResult : MaSchoeller.Dublin.Client.Proxies.Users.BaseResult
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PasswordChangeResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Communications.Mo" +
        "dels")]
    public partial class PasswordChangeResult : MaSchoeller.Dublin.Client.Proxies.Users.BaseResult
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OperationResult", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Database")]
    public enum OperationResult : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Lock = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotFound = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SaveFailure = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CascadingDeleteError = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlreadyExists = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotAuthorized = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotAuthenticated = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UnkownError = 2147483647,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/MaSchoeller.Dublin.Core.Models")]
    public partial class User : object
    {
        
        private string FirstnameField;
        
        private int IdField;
        
        private bool IsAdminField;
        
        private string LastnameField;
        
        private string UsernameField;
        
        private int VersionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Firstname
        {
            get
            {
                return this.FirstnameField;
            }
            set
            {
                this.FirstnameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAdmin
        {
            get
            {
                return this.IsAdminField;
            }
            set
            {
                this.IsAdminField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Lastname
        {
            get
            {
                return this.LastnameField;
            }
            set
            {
                this.LastnameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username
        {
            get
            {
                return this.UsernameField;
            }
            set
            {
                this.UsernameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Version
        {
            get
            {
                return this.VersionField;
            }
            set
            {
                this.VersionField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MaSchoeller.Dublin.Client.Proxies.Users.IUserService")]
    public interface IUserService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/ChangePassword", ReplyAction="http://tempuri.org/IUserService/ChangePasswordResponse")]
        MaSchoeller.Dublin.Client.Proxies.Users.PasswordChangeResult ChangePassword(string oldPassword, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/ChangePassword", ReplyAction="http://tempuri.org/IUserService/ChangePasswordResponse")]
        System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.PasswordChangeResult> ChangePasswordAsync(string oldPassword, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Login", ReplyAction="http://tempuri.org/IUserService/LoginResponse")]
        MaSchoeller.Dublin.Client.Proxies.Users.LoginResult Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Login", ReplyAction="http://tempuri.org/IUserService/LoginResponse")]
        System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.LoginResult> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SaveOrUpdateUser", ReplyAction="http://tempuri.org/IUserService/SaveOrUpdateUserResponse")]
        MaSchoeller.Dublin.Client.Proxies.Users.SaveOrUpdateResult SaveOrUpdateUser(MaSchoeller.Dublin.Client.Proxies.Users.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SaveOrUpdateUser", ReplyAction="http://tempuri.org/IUserService/SaveOrUpdateUserResponse")]
        System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.SaveOrUpdateResult> SaveOrUpdateUserAsync(MaSchoeller.Dublin.Client.Proxies.Users.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        MaSchoeller.Dublin.Client.Proxies.Users.DeleteResult DeleteUser(MaSchoeller.Dublin.Client.Proxies.Users.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.DeleteResult> DeleteUserAsync(MaSchoeller.Dublin.Client.Proxies.Users.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        System.Collections.Generic.List<MaSchoeller.Dublin.Client.Proxies.Users.User> GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MaSchoeller.Dublin.Client.Proxies.Users.User>> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SetAdminRights", ReplyAction="http://tempuri.org/IUserService/SetAdminRightsResponse")]
        MaSchoeller.Dublin.Client.Proxies.Users.AdminResult SetAdminRights(int userId, bool AdminRight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SetAdminRights", ReplyAction="http://tempuri.org/IUserService/SetAdminRightsResponse")]
        System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.AdminResult> SetAdminRightsAsync(int userId, bool AdminRight);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IUserServiceChannel : MaSchoeller.Dublin.Client.Proxies.Users.IUserService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<MaSchoeller.Dublin.Client.Proxies.Users.IUserService>, MaSchoeller.Dublin.Client.Proxies.Users.IUserService
    {
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public MaSchoeller.Dublin.Client.Proxies.Users.PasswordChangeResult ChangePassword(string oldPassword, string newPassword)
        {
            return base.Channel.ChangePassword(oldPassword, newPassword);
        }
        
        public System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.PasswordChangeResult> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            return base.Channel.ChangePasswordAsync(oldPassword, newPassword);
        }
        
        public MaSchoeller.Dublin.Client.Proxies.Users.LoginResult Login(string username, string password)
        {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.LoginResult> LoginAsync(string username, string password)
        {
            return base.Channel.LoginAsync(username, password);
        }
        
        public MaSchoeller.Dublin.Client.Proxies.Users.SaveOrUpdateResult SaveOrUpdateUser(MaSchoeller.Dublin.Client.Proxies.Users.User user)
        {
            return base.Channel.SaveOrUpdateUser(user);
        }
        
        public System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.SaveOrUpdateResult> SaveOrUpdateUserAsync(MaSchoeller.Dublin.Client.Proxies.Users.User user)
        {
            return base.Channel.SaveOrUpdateUserAsync(user);
        }
        
        public MaSchoeller.Dublin.Client.Proxies.Users.DeleteResult DeleteUser(MaSchoeller.Dublin.Client.Proxies.Users.User user)
        {
            return base.Channel.DeleteUser(user);
        }
        
        public System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.DeleteResult> DeleteUserAsync(MaSchoeller.Dublin.Client.Proxies.Users.User user)
        {
            return base.Channel.DeleteUserAsync(user);
        }
        
        public System.Collections.Generic.List<MaSchoeller.Dublin.Client.Proxies.Users.User> GetAllUsers()
        {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MaSchoeller.Dublin.Client.Proxies.Users.User>> GetAllUsersAsync()
        {
            return base.Channel.GetAllUsersAsync();
        }
        
        public MaSchoeller.Dublin.Client.Proxies.Users.AdminResult SetAdminRights(int userId, bool AdminRight)
        {
            return base.Channel.SetAdminRights(userId, AdminRight);
        }
        
        public System.Threading.Tasks.Task<MaSchoeller.Dublin.Client.Proxies.Users.AdminResult> SetAdminRightsAsync(int userId, bool AdminRight)
        {
            return base.Channel.SetAdminRightsAsync(userId, AdminRight);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}
