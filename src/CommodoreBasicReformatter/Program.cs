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
                Console.WriteLine();
                Console.WriteLine("NAME");
                Console.WriteLine("    CommodoreBasicReformatter");
                Console.WriteLine("SYNTAX");
                Console.WriteLine("    CommodoreBasicReformatter [--split-lines] <infile> <outfile>");
                Console.WriteLine("REMARKS");
                Console.WriteLine("    (c) 2019 Kasper B. Graversen and Jacob Dahl Pind");
                Console.WriteLine();
                return;
            }

            int i = 0;
            bool splitLines = false;
            if (args[i] == "--split-lines")
            {
                splitLines = true;
                i++;
            }
            var input = args[i++];
            var output = args[i++];

            var result = new Reformatter(new StmtsSplitter()).Reformat(File.ReadAllText(input), splitLines);
            File.WriteAllBytes(output, Encoding.ASCII.GetBytes(result));
        }
    }
}
