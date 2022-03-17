using System.Threading.Tasks;

namespace Spreetail.Demo.ConsoleCommands
{
    /// <summary>
    /// Represents a command requested from the console.
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Executes the console command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ExecuteAsync(string command);

        /// <summary>
        /// Gets the command supported by this instance.
        /// </summary>
        string SupportedCommand { get; }
    }
}
