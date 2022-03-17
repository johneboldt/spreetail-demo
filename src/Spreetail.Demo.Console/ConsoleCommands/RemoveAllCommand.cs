using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class RemoveAllCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public RemoveAllCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public override string SupportedCommand => "RemoveAll";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                ShowUsage();
                return;
            }

            var response = await _repository.RemoveAllAsync(parameters[0]);
            if (response.HasError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine("Removed");
        }
    }
}
