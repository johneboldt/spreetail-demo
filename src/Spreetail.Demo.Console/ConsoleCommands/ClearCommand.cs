using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class ClearCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public ClearCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "Clear";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length > 0)
            {
                ShowUsage();
                return;
            }

            var response = await _repository.ClearAsync();
            if (response.HasError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine("Cleared");
        }
    }
}
