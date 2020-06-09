﻿using FluentNHibernate.Conventions.Helpers.Prebuilt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public static class PasswordConverter
    {
        public const char ReplaceChar = '●';
        public static string PasswordChange(string source, string input)
        {
            var builder = new StringBuilder();
            var offset = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ReplaceChar)
                {
                    builder.Append(source[i + offset]);
                    
                }
                else
                {
                    builder.Append(input[i]);
                    offset--;
                }
            }
            return builder.ToString();
        }
    }
}
