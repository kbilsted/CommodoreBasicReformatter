namespace CommodoreBasicReformatter
{
    public class Configuration
    {
        public string Input, Output;
        public bool SplitLines, AddExplanations;

        public static Configuration Parse(string[] args)
        {
            var options = new Configuration();
            int i = 0;

            if (args[i] == "--split-lines")
            {
                options.SplitLines = true;
                i++;
            }

            if (args[i] == "--add-explanations")
            {
                options.AddExplanations = true;
                i++;
            }

            options.Input = args[i++];
            options.Output = args[i++];

            return options;
        }
    }
}