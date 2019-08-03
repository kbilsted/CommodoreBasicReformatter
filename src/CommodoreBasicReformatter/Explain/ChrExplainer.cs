using System.Collections.Generic;

namespace CommodoreBasicReformatter.Explain
{
    class ChrExplainer : IExplainer
    {
        static readonly string ChrExplanations = 
@"5| Changes the text color to white.
 8| Disables changing the character set using the SHIFT + Commodore key combination.
 9| Enables changing the character set using the SHIFT + Commodore key combination.
 13| Carriage return; next character will go in the first column of the following text line. As opposed to traditional ASCII-based system, no LINE FEED character needs to be sent in conjunction with this Carriage return character in the PETSCII system.
 14| Select the lowercase/uppercase character set.
 17| Cursor down: Next character will be printed in subsequent column one text line further down the screen.
 18| Reverse on: Selects reverse video text.
 19| Home: Next character will be printed in the upper left-hand corner of the screen.
 20| Delete, or ""backspace""; erases the previous character and moves the cursor one character position to the left.
 28| Changes the text color to red.
 29| Advances the cursor one character position without printing anything.
 30| Changes the text color to green.
 31| Changes the text color to blue.
 129| Changes the text color to orange.
 141| Same action as Carriage return (13/$0D).
 142| Select the uppercase/semigraphics character set.
 144| Changes the text color to black.
 145| Cursor up: Next character will be printed in subsequent column one text line further up the screen.
 146| Reverse off: De-selects reverse video text.
 147| Clears screen of any text, and causes the next character to be printed at the upper left-hand corner of the text screen.
 148| Insert: Makes room for extra characters at the current cursor position, by ""pushing"" existing characters at that position further to the right.
 149| Changes the text color to brown.
 150| Changes the text color to light red.
 151| Changes the text color to dark grey.
 152| Changes the text color to medium grey.
 153| Changes the text color to light green.
 154| Changes the text color to light blue.
 155| Changes the text color to light grey.
 156| Changes the text color to purple.
 157| Moves the cursor one character position backwards, without printing or deleting anything.
 158| Changes the text color to yellow.
 159| Changes the text color to cyan.";

        static readonly Dictionary<string, string> C64ChrMap = new ExplanationsConfigurationParser().BuildExplanations(ChrExplanations);

        public void AddExplanations(GrammarProgram program)
        {
            foreach (var line in program.Lines)
            {
                for (int stmtCnt = 0; stmtCnt < line.Stmts.Count; stmtCnt++)
                {
                    var block = line.Stmts[stmtCnt];

                    AnalyzeDirectArgTo_Chr(block, line);
                }
            }
        }

        static void AnalyzeDirectArgTo_Chr(GrammarStmt block, GrammarLine line)
        {
            for (int p = 0; p < block.Content.Count - 2; p++)
            {
                var chrArgument = block.Content[p + 2].Value;

                if (block.Content[p].Type == TokenKind.Keyword
                    && block.Content[p].Value == "chr$"
                    && C64ChrMap.ContainsKey(chrArgument))
                {
                    line.Explanations.Add($"{chrArgument}={C64ChrMap[chrArgument]}");
                }
            }
        }
    }
}