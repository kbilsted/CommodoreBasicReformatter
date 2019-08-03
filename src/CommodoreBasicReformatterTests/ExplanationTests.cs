using CommodoreBasicReformatter;
using CommodoreBasicReformatter.Explain;
using Xunit;

namespace CommodoreBasicReformatterTests
{
    public class ExplanationTests
    {
        Reformatter Create()
        {
            return new Reformatter(new Grammar(), new StmtsSplitter(), new Explainer());
        }

        [Fact]
        public void ExplainProgram_ragingrobots_subset()
        {
            var actual = Create().Reformat(
@"10 X = X +1: IF X>646 THEN X=0
15 W = 53280
20 POKE 53281,X: POKE 646,X : POKE W,0
40 FOR Z=0 TO 150: NEXT Z: GOTO 10", new Configuration(){AddExplanations = true});

            Assert.Equal(
@"10 X = X+1 : if X>646 then X = 0
15 rem 53280=Border color
15 W = 53280
20 rem 53281=Background color
20 rem 646=Current cursor color
20 poke 53281,X : poke 646,X : poke W,0
40 for Z = 0 to 150 : next Z : goto 10", actual.Trim());
        }

        [Fact]
        public void ExplainProgram_Basic_fullscreen_scroller()
        {
            var actual = Create().Reformat(
@"10 print chr$(147) : poke53280,6:poke53281,0
20 for l=1024 to 2023:poke l,219:next l
30 for l=0 to 7
40 poke 53265, (peek(53265) and 240) or 7-l : poke 53270,l
50 next l
60 goto 30", new Configuration() { AddExplanations = true });

            Assert.Equal(
@"10 rem 53280=Border color
10 rem 53281=Background color
10 rem 147=Clears screen of any text, and causes the next character to be printed at the upper left-hand corner of the text screen.
10 print chr$(147) : poke 53280,6 : poke 53281,0
20 rem 1024=Default area of screen memory
20 rem 2023=Default area of screen memory
20 for l = 1024 to 2023 : poke l,219 : next l
30 for l = 0 to 7
40 rem 53265=Screen control register #1
40 rem 53270=Screen control register #2
40 poke 53265,(peek(53265)and 240)or 7-l : poke 53270,l
50 next l
60 goto 30", actual.Trim());
        }

        [Fact]
        public void DoNotExplainDataLines()
        {
            var actual = Create()
                .Reformat("10 DATA 20,53281,4096", new Configuration() { AddExplanations = true });

            Assert.Equal("10 data 20,53281,4096", actual.Trim());
        }

        [Fact]
        public void Explain_Chr()
        {
            var fileContent = "10 PRINT CHR$(147)";
            var actual = Create()
                .Reformat(fileContent, new Configuration() { AddExplanations = true });

            Assert.Equal(
@"10 rem 147=Clears screen of any text, and causes the next character to be printed at the upper left-hand corner of the text screen.
10 print chr$(147)", actual.Trim());
        }

        [Fact]
        public void Explain_Poke()
        {
            var fileContent = "10 POKE 53281,1";
            var actual = Create()
                .Reformat(fileContent, new Configuration() { AddExplanations = true });

            Assert.Equal(
                @"10 rem 53281=Background color
10 poke 53281,1", actual.Trim());
        }

        [Fact]
        public void Explain_only_once_Poke()
        {
            var fileContent = "10 POKE 53281,1 : POKE 53281,1";
            var actual = Create()
                .Reformat(fileContent, new Configuration() { AddExplanations = true });

            Assert.Equal(
                @"10 rem 53281=Background color
10 poke 53281,1 : poke 53281,1", actual.Trim());
        }


        [Theory]
        [InlineData(@"5 W=53281", @"5 W = 53281")]
        [InlineData(
@"5 W=53281
10 POKE W,1",
@"5 rem 53281=Background color
5 W = 53281
10 poke W,1")]
        [InlineData(
@"10 W=53281 : POKE W,1", 
@"10 rem 53281=Background color
10 W = 53281 : poke W,1")]
        [InlineData("20 for l=1024 to 2023:poke l,219:next l",
@"20 rem 1024=Default area of screen memory
20 rem 2023=Default area of screen memory
20 for l = 1024 to 2023 : poke l,219 : next l")]
        public void ExplainVariableIfUsedIn_Poke(string input, string expected)
        {
            var actual = Create()
                .Reformat(input, new Configuration() { AddExplanations = true });

            Assert.Equal(expected, actual.Trim());
        }

        [Theory]
        [InlineData(@"5 W=53281", @"5 W = 53281")]
        [InlineData(
            @"5 W=53281
10 SYS W",
            @"5 rem 53281=Background color
5 W = 53281
10 sys W")]
        [InlineData(
            @"10 W=53281 : SYS W",
            @"10 rem 53281=Background color
10 W = 53281 : sys W")]
        [InlineData("20 for l=1024 to 2023:sys l:next l",
            @"20 rem 1024=Default area of screen memory
20 rem 2023=Default area of screen memory
20 for l = 1024 to 2023 : sys l : next l")]
        public void ExplainVariableIfUsedIn_Sys(string input, string expected)
        {
            var actual = Create()
                .Reformat(input, new Configuration() { AddExplanations = true });

            Assert.Equal(expected, actual.Trim());
        }

        [Theory]
        [InlineData(@"5 W=53281", @"5 W = 53281")]
        [InlineData(
            @"5 W=53281
10 WAIT W",
            @"5 rem 53281=Background color
5 W = 53281
10 wait W")]
        [InlineData(
            @"10 W=53281 : WAIT W",
            @"10 rem 53281=Background color
10 W = 53281 : wait W")]
        [InlineData("20 for l=1024 to 2023:wait l:next l",
            @"20 rem 1024=Default area of screen memory
20 rem 2023=Default area of screen memory
20 for l = 1024 to 2023 : wait l : next l")]
        public void ExplainVariableIfUsedIn_Wait(string input, string expected)
        {
            var actual = Create()
                .Reformat(input, new Configuration() { AddExplanations = true });

            Assert.Equal(expected, actual.Trim());
        }

        [Theory]
        [InlineData(@"5 W=53281", @"5 W = 53281")]
        [InlineData(
            @"5 W=53281
10 PRINT PEEK(W)",
            @"5 rem 53281=Background color
5 W = 53281
10 print peek(W)")]
        [InlineData(
            @"10 W=53281 : PRINT PEEK (W)",
            @"10 rem 53281=Background color
10 W = 53281 : print peek(W)")]
        [InlineData("20 for l=1024 to 2023:peek(l):next l",
            @"20 rem 1024=Default area of screen memory
20 rem 2023=Default area of screen memory
20 for l = 1024 to 2023 : peek(l) : next l")]
        public void ExplainVariableIfUsedIn_Peek(string input, string expected)
        {
            var actual = Create()
                .Reformat(input, new Configuration() { AddExplanations = true });

            Assert.Equal(expected, actual.Trim());
        }


        [Fact]
        public void ExplainPeek()
        {
            var fileContent = "10 ys=peek(895)";
            var actual = Create()
                .Reformat(fileContent, new Configuration() { AddExplanations = true });

            Assert.Equal(
@"10 rem 895=Datasette buffer
10 ys = peek(895)", actual.Trim());
        }
    }
}