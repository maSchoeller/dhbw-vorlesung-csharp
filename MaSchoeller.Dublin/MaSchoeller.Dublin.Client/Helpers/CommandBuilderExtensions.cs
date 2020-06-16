using MaSchoeller.Extensions.Desktop.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public static class CommandBuilderExtensions
    {
        public static ICommandBuilder ObserveCommandManager(this ICommandBuilder builder)
        {
            builder.AddObserver(CommandManagerObserver.Get());
            return builder;
        }
    }
}
