using Spreetail.Demo.ConsoleCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spreetail.Demo
{
    public class StartupService : IStartupService
    {
        private Dictionary<string, IConsoleCommand> _commands;

        public StartupService(IEnumerable<IConsoleCommand> consoleCommands)
        {
            if (consoleCommands is null)
            {
                throw new ArgumentNullException(nameof(consoleCommands));
            }

            _commands = new Dictionary<string, IConsoleCommand>();
            foreach (var command in consoleCommands)
            {
                if (_commands.ContainsKey(command.SupportedCommand)) throw new InvalidOperationException($"Duplicate command handler found for {command.SupportedCommand}");
                _commands[command.SupportedCommand.ToLower()] = command;
            }
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Welcome to the Multi-Value dictionary console app! For help type 'help'.");
            while (true)
            {
                Console.WriteLine();
                Console.Write("> ");
                var commandWithParameters = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commandWithParameters)) continue;
                var command = commandWithParameters.Split(' ').FirstOrDefault();
                if (command is null) continue;
                if (command.ToLower() == "exit") return;
                command = command.ToLower();
                if (!_commands.ContainsKey(command)) continue;
                await _commands[command].ExecuteAsync(commandWithParameters);
            }
        }
    }
}
