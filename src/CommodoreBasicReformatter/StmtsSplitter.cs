using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace CommodoreBasicReformatter
{
    public class StmtsSplitter
    {
        internal void SplitLines(GrammarProgram program)
        {
            List<GrammarLine> astLines = program.Lines;
            for (int l = 0; l < astLines.Count; l++)
            {
                var line = astLines[l];

                if (line.Stmts.Count > 1)
                {
                    int newlinenumber = line.LineNumber + 1;
                    int insertpos = l + 1;
                    while (line.Stmts.Count > 1)
                    {
                        if (IsRemark(line.Stmts[1].Content[0]))
                            break;

                        var token = line.Stmts[0].Content[0];
                        bool isIfThen = token.Type == TokenKind.Keyword && token.Value == "if";
                        if (isIfThen)
                            break;

                        var restOfLineExcludingRemark = line.Stmts
                            .Skip(1)
                            .Where(x => !IsRemark(x.Content[0]))
                            .ToList();
                        astLines.Insert(insertpos++, new GrammarLine(newlinenumber++, restOfLineExcludingRemark));

                        var firstStmtWithRemark = new[] {line.Stmts[0]}
                            .Union(line.Stmts.Where(x => IsRemark(x.Content[0])))
                            .ToList();
                        line.Stmts = firstStmtWithRemark;
                    }
                }
            }
        }

        bool IsRemark(Token token)
        {
            return token.Type == TokenKind.Keyword && token.Value.StartsWith("rem");
        }
    }
}