using MaSchoeller.Dublin.Core.Services;
using System.ServiceModel;

namespace MaSchoeller.Dublin.Core.Communications
{
    internal abstract class BaseService
    {
        protected ISecurityHelper SecurityHelper { get; }
        public BaseService(ISecurityHelper helper)
        {
            SecurityHelper = helper;
        }

        private UserContext _user = new UserContext();
        protected UserContext User
        {
            get
            {
                if (!_user.Inialized)
                {
                    var token = GetToken();
                    if (!(token is null))
                    {
                        var user = SecurityHelper.GetUserContext(token);
                        if (!(user is null)) _user = user;
                    }
                    _user.Inialized = true;
                }
                return _user;
            }
        }

        protected bool Validate()
        {
            var token = GetToken();
            if (token is null)
            {
                return false;
            }
            return SecurityHelper.ValidateToken(token);
        }

        private string? GetToken()
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("TokenHeader", "TokenNameSpace") == -1)
            {
                return null;
            }
            return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace");
        }
    }
}
