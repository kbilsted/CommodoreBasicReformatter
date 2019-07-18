using System;
using System.IO;
using System.Text;

namespace CommodoreBasicReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Missing arguments!");
                Console.WriteLine("    USAGE");
                Console.WriteLine("CommodoreBasicReformatter <infile> <outfile>");
                return;
            }

            var result = new Reformatter().Reformat(File.ReadAllText(args[0]), false);

            File.WriteAllBytes(args[1], Encoding.ASCII.GetBytes(result));
        }
    }
}
