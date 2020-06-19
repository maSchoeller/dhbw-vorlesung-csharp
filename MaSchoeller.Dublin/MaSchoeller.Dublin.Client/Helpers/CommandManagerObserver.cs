using MaSchoeller.Extensions.Desktop.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public class CommandManagerObserver : ICommandObserver
    {
        public event Action<object, EventArgs>? Changed;

        public CommandManagerObserver()
        {
            CommandManager.RequerySuggested +=
                (s, e) => Changed?.Invoke(this, EventArgs.Empty);
        }

        private static readonly ICommandObserver _observer = new CommandManagerObserver();
        public static ICommandObserver Get() => _observer;
    }

    public static class CommandBuilderExtensions
    {
        public static ICommandBuilder ObserveCommandManager(this ICommandBuilder builder)
        {
            builder.AddObserver(CommandManagerObserver.Get());
            return builder;
        }
    }
}
