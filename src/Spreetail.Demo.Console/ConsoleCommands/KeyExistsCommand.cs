using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class KeyExistsCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public KeyExistsCommand(IMultiValueDictionaryRepository<string, string> repository, IUsageService usageService) : base(usageService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public override string SupportedCommand => "KeyExists";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                ShowUsage();
                return;
            }

            Console.WriteLine(await _repository.KeyExistsAsync(parameters[0]));

        }
    }
}
