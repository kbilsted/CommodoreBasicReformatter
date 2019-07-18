using System;
using System.IO;

namespace CommodoreBasicReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new Reformatter().Reformat(File.ReadAllText(args[0]));
            if (args.Length == 0)
            {
                Console.WriteLine("Missing arguments!");
                Console.WriteLine("    USAGE");
                Console.WriteLine("CommodoreBasicReformatter <basicfile>");
                return;
            }

            Console.WriteLine(result);
        }
    }
}
