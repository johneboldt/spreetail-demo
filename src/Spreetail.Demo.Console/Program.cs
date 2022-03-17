using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spreetail.Demo;
using Spreetail.Demo.ConsoleCommands;
using Spreetail.Demo.Repository;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMultiValueDictionaryRepository<string, string>, MultiValueDictionaryInMemoryRepository<string, string>>();
                    services.AddSingleton<IConsoleCommand, KeysCommand>();
                    services.AddSingleton<IConsoleCommand, AddCommand>();
                    services.AddSingleton<IConsoleCommand, MembersCommand>();
                    services.AddSingleton<IConsoleCommand, RemoveCommand>();
                    services.AddSingleton<IConsoleCommand, RemoveAllCommand>();
                    services.AddSingleton<IConsoleCommand, ClearCommand>();
                    services.AddSingleton<IConsoleCommand, KeyExistsCommand>();
                    services.AddSingleton<IConsoleCommand, MemberExistsCommand>();
                    services.AddSingleton<IConsoleCommand, AllMembersCommand>();
                    services.AddSingleton<IConsoleCommand, ItemsCommand>();
                    services.AddSingleton<IStartupService, StartupService>();
                    services.AddSingleton<IUsageService, UsageService>();
                })
                .Build();
            var svc = host.Services.GetRequiredService<IStartupService>();
            await svc.RunAsync();
        }
    }
}
