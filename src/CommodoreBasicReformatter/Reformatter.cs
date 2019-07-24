using System;
using System.Linq;
using System.Text;
using CommodoreBasicReformatter.Explain;

namespace CommodoreBasicReformatter
{
    public class Reformatter
    {
        readonly Grammar grammar;
        readonly StmtsSplitter splitter;
        readonly IExplainer explainer;

        public Reformatter(Grammar grammar, StmtsSplitter splitter, IExplainer explainer)
        {
            this.grammar = grammar;
            this.splitter = splitter;
            this.explainer = explainer;
        }

        public string Reformat(string fileContent, Configuration configuration)
        {
            if (!fileContent.EndsWith('\n'))
                fileContent += "\n";

            var astLines = grammar.Parse(fileContent);

            if (configuration.SplitLines)
                splitter.SplitLines(astLines);

            if(configuration.AddExplanations)
                explainer.AddExplanations(astLines);

            var sb = new StringBuilder();
            astLines.Lines.ForEach(x => sb.AppendLine(Format(x)));
            return sb.ToString();
        }

        static string Format(GrammarLine l)
        {
            var stmts = string.Join(" : ", l.Stmts.Select(x => Format(x)));

            var explanation=new StringBuilder();
            l.Explanations
                .Distinct().ToList()
                .ForEach(x => explanation.AppendLine($"{l.LineNumber} rem {x}"));

            return  $"{explanation}{l.LineNumber} {stmts}";
        }

        static string Format(GrammarStmt l)
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