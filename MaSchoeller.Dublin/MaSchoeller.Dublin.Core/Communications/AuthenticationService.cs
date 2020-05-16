using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthenticationService" in both code and config file together.
    public class AuthenticationService : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "Marvin" && password != "admin")
            {
                throw new SecurityTokenException();
            }
        }
    }
}
