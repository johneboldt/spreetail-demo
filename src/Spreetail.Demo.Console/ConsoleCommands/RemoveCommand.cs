using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class RemoveCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public RemoveCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "Remove";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                ShowUsage();
                return;
            }

            var response = await _repository.RemoveMemberAsync(parameters[0], parameters[1]);
            if (response.HasError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine("Removed");
        }
    }
}
