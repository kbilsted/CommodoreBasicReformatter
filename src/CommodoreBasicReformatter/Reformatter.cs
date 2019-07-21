using System.Linq;
using System.Text;

namespace CommodoreBasicReformatter
{
    public class Reformatter
    {
        readonly Grammar grammer;
        readonly StmtsSplitter splitter;

        public Reformatter(Grammar grammer, StmtsSplitter splitter)
        {
            this.grammer = grammer;
            this.splitter = splitter;
        }

        public string Reformat(string fileContent, bool splitLines)
        {
            var astLines = grammer.Parse(fileContent);

            if (splitLines)
                splitter.SplitLines(astLines);

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

    }
}