using Spreetail.Demo.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class AllMembersCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public AllMembersCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public override string SupportedCommand => "AllMembers";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 0)
            {
                ShowUsage();
                return;
            }

            var members = await _repository.GetAllMembersAsync();
            if (!members.Any())
            {
                Console.WriteLine("(empty set)");
                return;
            }

            var i = 0;
            foreach (var member in members)
            {
                i++;
                Console.WriteLine($"{i}) {member}");
            }
        }
    }
}
