using System;
using System.IO;
using System.Text;
using CommodoreBasicReformatter.Explain;

namespace CommodoreBasicReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 && args.Length > 4)
            {
                PrintHelp();
                return;
            }

            var options = Configuration.Parse(args);

            var result = new Reformatter(new Grammar(), new StmtsSplitter(), new Explainer())
                .Reformat(File.ReadAllText(options.Input), options);

            File.WriteAllBytes(options.Output, Encoding.ASCII.GetBytes(result));
        }


        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("NAME");
            Console.WriteLine("    CommodoreBasicReformatter");
            Console.WriteLine("VERSION");
            Console.WriteLine("    1.02");
            Console.WriteLine("SYNTAX");
            Console.WriteLine("    CommodoreBasicReformatter [--split-lines] [--add-explanations] <infile> <outfile>");
            Console.WriteLine("REMARKS");
            Console.WriteLine("    (c) 2019 Kasper B. Graversen");
            Console.WriteLine();
        }
    }
}
