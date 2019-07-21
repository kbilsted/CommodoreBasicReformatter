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
            astLines.ForEach(x => sb.AppendLine(Format(x)));
            return sb.ToString();
        }

        private static string Format(GrammarLine l)
        {
            var stmts = string.Join(" : ", l.Stmts.Select(x => Format(x)));
            return $"{l.LineNumber} {stmts}";
        }

        private static string Format(GrammarStmt l)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < l.Content.Count - 1; i++)
            {
                var current = l.Content[i];
                sb.Append(current.Value);

                if (current.Type == TokenKind.Symbol && current.Value != "=")
                    continue;

                var next = l.Content[i + 1];
                if (next.Type != TokenKind.Symbol || next.Value == "=")
                    sb.Append(" ");
            }

            sb.Append(l.Content.Last().Value);

            return sb.ToString();
        }

        private static void SplitLines(List<GrammarLine> astLines)
        {
            for (int l = 0; l < astLines.Count; l++)
            {
                var line = astLines[l];

                if (line.Stmts.Count > 1)
                {
                    int newlinenumber = line.LineNumber + 1;
                    int insertpos = l + 1;
                    while (line.Stmts.Count > 1)
                    {
                        var token = line.Stmts[1].Content[0];
                        bool isRemark = token.Type == TokenKind.Keyword && token.Value.StartsWith("rem");
                        if (isRemark)
                            break;

                        astLines.Insert(insertpos++, new GrammarLine(newlinenumber++, new List<GrammarStmt> {line.Stmts[1]}));
                        line.Stmts.RemoveAt(1);
                    }
                }
            }
        }
    }
}