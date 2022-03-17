using System;

namespace Spreetail.Demo
{
    public class UsageService : IUsageService
    {
        public void DisplayUsage()
        {
            Console.WriteLine();
            Console.WriteLine("Usages:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Add key member");
            Console.ResetColor();
            Console.WriteLine("Adds a member to a collection for a given key. Displays an error if the member already exists for the key.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Remove key member");
            Console.ResetColor();
            Console.WriteLine("Removes a member from a collection for a given key. Displays an error if the key doesn't exist or the member doesn't exist for the key.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("RemoveAll key");
            Console.ResetColor();
            Console.WriteLine("Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Clear");
            Console.ResetColor();
            Console.WriteLine("Removes all keys and all members from the dictionary.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Keys");
            Console.ResetColor();
            Console.WriteLine("Returns all the keys in the dictionary. Order is not guaranteed.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("KeyExists key");
            Console.ResetColor();
            Console.WriteLine("Returns whether a key exists or not.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("MemberExists key member");
            Console.ResetColor();
            Console.WriteLine("Returns whether a member exists within a key. Returns false if the key does not exist.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("MemberExists key member");
            Console.ResetColor();
            Console.WriteLine("Returns whether a member exists within a key. Returns false if the key does not exist.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("AllMembers");
            Console.ResetColor();
            Console.WriteLine("Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Items");
            Console.ResetColor();
            Console.WriteLine("Returns all keys in the dictionary and all of their members. Returns nothing if there are none. Order is not guaranteed.\n");
        }
    }
}
