using System;
using System.IO;
using System.Text;

namespace CommodoreBasicReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                PrintHelp();
                return;
            }

            var options = Options.Parse(args);

            var result = new Reformatter(new Grammar(), new StmtsSplitter())
                .Reformat(File.ReadAllText(options.Input), options.SplitLines);

            File.WriteAllBytes(options.Output, Encoding.ASCII.GetBytes(result));
        }

        class Options
        {
            public string Input, Output;
            public bool SplitLines;

            public static Options Parse(string[] args)
            {
                var options = new Options();
                int i = 0;

                if (args[i] == "--split-lines")
                {
                    options.SplitLines = true;
                    i++;
                }

                options.Input = args[i++];
                options.Output = args[i++];

                return options;
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("NAME");
            Console.WriteLine("    CommodoreBasicReformatter");
            Console.WriteLine("VERSION");
            Console.WriteLine("    1.01");
            Console.WriteLine("SYNTAX");
            Console.WriteLine("    CommodoreBasicReformatter [--split-lines] <infile> <outfile>");
            Console.WriteLine("REMARKS");
            Console.WriteLine("    (c) 2019 Kasper B. Graversen");
            Console.WriteLine();
        }
    }
}
