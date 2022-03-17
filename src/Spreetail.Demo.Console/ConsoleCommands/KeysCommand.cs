using Spreetail.Demo.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class KeysCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public KeysCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "Keys";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length > 1)
            {
                ShowUsage();
                return;
            }

            var keys = await _repository.GetKeysAsync();
            if (!keys.Any())
            {
                Console.WriteLine("(empty set)");
                return;
            }

            var i = 0;
            foreach (var key in keys)
            {
                i++;
                Console.WriteLine($"{i}) {key}");
            }
        }
    }
}
