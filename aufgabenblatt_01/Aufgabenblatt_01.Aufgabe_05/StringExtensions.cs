using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_05
{
    public static class StringExtensions
    {
        public static int CountWords(this string str) 
            => str.Split('\t', ' ').Count();
    }
}
