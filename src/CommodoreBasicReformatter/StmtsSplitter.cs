using System.Collections.Generic;

namespace CommodoreBasicReformatter
{
    public class StmtsSplitter
    {
        internal void SplitLines(List<GrammarLine> astLines)
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

                        astLines.Insert(insertpos++, new GrammarLine(newlinenumber++, new List<GrammarStmt> { line.Stmts[1] }));
                        line.Stmts.RemoveAt(1);
                    }
                }
            }
        }
    }
}