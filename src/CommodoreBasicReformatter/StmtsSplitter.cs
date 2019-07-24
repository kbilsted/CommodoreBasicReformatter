using System.Collections.Generic;
using System.Linq;

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
                        if (line.Stmts[1].Content[0].IsRemark())
                            break;

                        var token = line.Stmts[0].Content[0];
                        bool isIfThen = token.Type == TokenKind.Keyword && token.Value == "if";
                        if (isIfThen)
                            break;

                        var restOfLineExcludingRemark = line.Stmts
                            .Skip(1)
                            .Where(x => !x.Content[0].IsRemark())
                            .ToList();
                        astLines.Insert(insertpos++, new GrammarLine(newlinenumber++, restOfLineExcludingRemark));

                        var firstStmtWithRemark = new[] {line.Stmts[0]}
                            .Union(line.Stmts.Where(x => x.Content[0].IsRemark()))
                            .ToList();
                        line.Stmts = firstStmtWithRemark;
                    }
                }
            }
        }

        
    }
}