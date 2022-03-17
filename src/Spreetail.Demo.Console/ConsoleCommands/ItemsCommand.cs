using Spreetail.Demo.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    public class ItemsCommand : ConsoleCommand
    {
        private IMultiValueDictionaryRepository<string, string> _repository;

        public ItemsCommand(IMultiValueDictionaryRepository<string, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public override string SupportedCommand => "Items";

        protected async override Task PerformCommandAsync(string[] parameters)
        {
            if (parameters.Length != 0)
            {
                ShowUsage();
                return;
            }

            var items = await _repository.GetAllItemsAsync();
            if (!items.Any())
            {
                Console.WriteLine("(empty set)");
                return;
            }

            var i = 0;
            foreach (var item in items)
            {
                foreach (var line in item.Members.Select(m => $"{++i}) {item.Key}: {m}")) Console.WriteLine(line);
            }
        }
    }
}
