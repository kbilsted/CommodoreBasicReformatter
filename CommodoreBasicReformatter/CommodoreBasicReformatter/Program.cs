using System;
using System.IO;

namespace CommodoreBasicReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new Reformatter().Reformat(File.ReadAllText(args[0]));
            Console.WriteLine(result);
        }
    }
}
