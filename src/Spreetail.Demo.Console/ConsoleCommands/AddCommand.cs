using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class AddCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public AddCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "Add";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                ShowUsage();
                return;
            }

            var response = await _repository.AddAsync(parameters[0], parameters[1]);
            if (response.HasError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine("Added");
        }
    }
}
