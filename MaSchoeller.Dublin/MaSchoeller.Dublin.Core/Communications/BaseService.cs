using MaSchoeller.Dublin.Core.Abstracts;
using MaSchoeller.Dublin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications
{
    internal abstract class BaseService
    {
        protected ISecurityHelper SecurityHelper { get; }
        public BaseService(ISecurityHelper helper)
        {
            SecurityHelper = helper;
        }

        protected bool Validate()
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("TokenHeader", "TokenNameSpace") == -1)
            {
                return false;
            }
            var token = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace");
            if (token is null)
            {
                return false;
            }
            return SecurityHelper.ValidateToken(token);
        }
    }
}
