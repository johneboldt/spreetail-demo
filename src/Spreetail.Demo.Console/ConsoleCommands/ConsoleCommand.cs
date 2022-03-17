using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public abstract class ConsoleCommand : IConsoleCommand
    {
        public abstract string SupportedCommand { get; }

        public async Task ExecuteAsync(string command)
        {
            var parameters = Regex.Split(command, @"(?<match>\w+)|\""(?<match>[\w\s]*)""").Skip(1).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            if (parameters.Length < 1)
            {
                ShowUsage();
                return;
            }

            await PerformCommandAsync(parameters.Skip(1).ToArray());
        }

        protected abstract Task PerformCommandAsync(string[] parameters);

        protected void ShowUsage()
        {
            Console.WriteLine("Usage:");
        }
    }
}
