using Microsoft.VisualBasic;

namespace MaSchoeller.Dublin.Core.Communications
{
    public class UserContext
    {
        public UserContext()
        {

        }


        public bool Inialized { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}