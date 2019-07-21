using System.Linq;
using CommodoreBasicReformatter;
using Xunit;

namespace CommodoreBasicReformatterTests
{
    public class TokenizerTests
    {
        [Fact]
        public void TokenizeListing()
        {
            var tokens = new Tokenizer("30 fori=atob").ReadAll();
            var actual = tokens.Select(x => x.Type).ToArray();

            Assert.Equal("a", tokens[4].Value);
            Assert.Equal(TokenKind.Name, tokens[4].Type);

            Assert.Equal("to", tokens[5].Value);
            Assert.Equal(TokenKind.Keyword, tokens[5].Type);

            Assert.Equal("b", tokens[6].Value);
            Assert.Equal(TokenKind.Name, tokens[6].Type);

            Assert.Equal(new[]
            {
                TokenKind.Digit,
                TokenKind.Keyword,
                TokenKind.Name,
                TokenKind.Symbol,
                TokenKind.Name,
                TokenKind.Keyword,
                TokenKind.Name,
                TokenKind.EOF
            }, actual);
        }

        [Fact]
        public void TokenizeRemLines()
        {
            var tokenizer = new Tokenizer(@"1 REM*********************************
");
            var tokens = tokenizer.ReadAll();
            Assert.Equal(new[]
            {
                TokenKind.Digit,
                TokenKind.Keyword,
                TokenKind.NewLine,
                TokenKind.EOF
            }, tokens.Select(x => x.Type).ToArray());
        }
    }
}