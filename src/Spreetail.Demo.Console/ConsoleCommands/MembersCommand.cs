using Spreetail.Demo.Repository;
using System;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class MembersCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public MembersCommand(IMultiValueDictionaryRepository<string, string> repository, IUsageService usageService) : base(usageService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override string SupportedCommand => "Members";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                ShowUsage();
                return;
            }

            var response = await _repository.GetMembersAsync(parameters[0]);
            if (response.HasError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            var i = 0;
            foreach (var member in response.Members)
            {
                i++;
                Console.WriteLine($"{i}) {member}");
            }
        }
    }
}
