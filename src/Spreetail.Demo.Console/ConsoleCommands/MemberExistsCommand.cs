using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class MemberExistsCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public MemberExistsCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "MemberExists";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                ShowUsage();
                return;
            }

            Console.WriteLine(await _repository.MemberExistsAsync(parameters[0], parameters[1]));
        }
    }
}
