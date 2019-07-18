using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommodoreBasicReformatter
{
    public class Reformatter
    {
        public string Reformat(string fileContent, bool splitLines)
        {
            var g = new Grammar(fileContent);
            var astLines = g.Parse();

            if (splitLines)
                SplitLines(astLines);

            var sb = new StringBuilder();
            astLines.ForEach(x => sb.AppendLine(x.ToString()));
            return sb.ToString();
        }

        private static void SplitLines(List<GrammarLine> astLines)
        {
            for (int l = 0; l < astLines.Count; l++)
            {
                var line = astLines[l];

                if (line.Content.Count > 1)
                {
                    int newlinenumber = line.LineNumber + 1;
                    int insertpos = l + 1;
                    while (line.Content.Count > 1)
                    {
                        if (line.Content[1].Content[0].Type == TokenKind.Keyword && line.Content[1].Content[0].Value.StartsWith("rem"))
                            break;

                        astLines.Insert(insertpos++, new GrammarLine(newlinenumber++, new List<GramarStmt> {line.Content[1]}));
                        line.Content.RemoveAt(1);
                    }
                }
            }
        }
    }
}