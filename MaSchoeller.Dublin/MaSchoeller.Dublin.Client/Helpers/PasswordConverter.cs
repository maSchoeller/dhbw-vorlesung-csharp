using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public static class PasswordConverter
    {
        public const char ReplaceChar = '*';
        public static string PasswordChange(string source, string input)
        {
            var dif = source.Length - input.Length;            
            if (dif <= 0)
            {
                var newSection = input.Trim(ReplaceChar);
                source += newSection;
                return source;
            }
            else
            {
                return source.Substring(0, source.Length - dif);
            }
        }
    }
}
