using System;
using System.Collections.Generic;
using System.Linq;

namespace CommodoreBasicReformatter.Explain
{
    class ExplanationsConfigurationParser
    {
        public Dictionary<string, string> BuildExplanations(string explanations)
        {
            var lines = explanations
                .Replace("\r", "")
                .Split("\n")
                .Select(line => line.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray());

            var result = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                ParseStartEnd(line, out var start, out var end);

                for (int address = start; address <= end; address++)
                {
                    foreach (var key in new[] { $"{address}", $"${address:X}" })
                    {
                        if (result.ContainsKey(key))
                            result[key] += " OR " + line[1];
                        else
                            result[key] = line[1];
                    }
                }
            }

            return result;
        }

        static void ParseStartEnd(string[] line, out int start, out int end)
        {
            if (int.TryParse(line[0], out start))
            {
                end = start;
            }
            else
            {
                var startEnd = line[0].Split('-');
                start = int.Parse(startEnd[0]);
                end = int.Parse(startEnd[1]);
            }
        }
    }
}
