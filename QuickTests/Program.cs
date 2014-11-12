using CSGODemoParser.Demo;
using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class Program
    {
        private static Dictionary<String, ICommand> commands = new Dictionary<String, ICommand>()
        {
            {"convars", new ConvarDumper()} //this is so not efficient when you get multiple commmands..
        };

        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Usage: programname commandname argument1 argument2");
                return;
            }
            String method = args[0];
            if (commands.ContainsKey(method))
            {
                commands[method].Execute(args);
            }
            else
            {
                Console.WriteLine("Unknown command " + args[0]);
            }
            Console.ReadLine();
        }
    }
}
