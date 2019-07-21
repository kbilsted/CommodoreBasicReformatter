using System;
using System.Collections.Generic;
using System.Linq;

namespace CommodoreBasicReformatter
{
    public enum TokenKind
    {
        Digit,
        Colon,
        Keyword,
        Symbol,
        String,
        NewLine,
        Name,
        EOF,
    }

    public class Token
    {
        public TokenKind Type;
        public string Value;

        public Token(TokenKind type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Tokenizer
    {
        public string Text;
        public int pos { get; private set; }

        public Tokenizer(string text)
        {
            Text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            pos = 0;
        }

        public List<Token> ReadAll()
        {
            var result = new List<Token>();
            do
            {
                result.Add(Next());
            } while (result.Last().Type != TokenKind.EOF);

            return result;
        }

        Token Next()
        {
            SkipSpaces();

            Token t;

            if ((t = ParseEOf()) != null)
                return t;
            if ((t = ParseNewline()) != null)
                return t;
            if ((t = ParseString()) != null)
                return t;
            if ((t = ParseDigit()) != null)
                return t;
            if ((t = ParseColon()) != null)
                return t;
            if ((t = ParseSymbol()) != null)
                return t;
            if ((t = ParseKeyword()) != null)
                return t;
            if ((t = ParseName()) != null)
                return t;

            throw new Exception("Stopped at " + pos + " ..." + Text.Substring(pos, Math.Min(25, Text.Length - pos)) + "...");
        }

        Token ParseEOf()
        {
            if (pos != Text.Length)
                return null;
            return new Token(TokenKind.EOF, null);
        }

        void SkipSpaces()
        {
            while (pos < Text.Length && Text[pos] == ' ')
                pos++;
        }

        Token ParseDigit()
        {
            if (!char.IsDigit(Text[pos]))
                return null;

            int i = 1;
            while (pos + i < Text.Length && char.IsDigit(Text[pos + i]))
                i++;

            var v = int.Parse(Text.Substring(pos, i));

            pos += i;
            return new Token(TokenKind.Digit, v.ToString());
        }

        Token ParseName()
        {
            if (!char.IsLetter(Text[pos]))
                return null;

            int i = 1;
            while (pos + i < Text.Length 
                   && (char.IsLetterOrDigit(Text[pos + i]) || Text[pos + i] == '$' || Text[pos + i] == '%') 
                   && GetKeywordAt(pos+i) == null)
                i++;

            var t = new Token(TokenKind.Name, Text.Substring(pos, i));
            pos += i;
            return t;
        }

        Token ParseString()
        {
            if (Text[pos] != '"')
                return null;

            var lastQuote = Text.IndexOf('"', pos + 1);
            var v = Text.Substring(pos, lastQuote + 1 - pos);

            pos += v.Length;
            return new Token(TokenKind.String, v);
        }

        Token ParseNewline()
        {
            if (Text[pos] != '\n')
                return null;

            pos++;
            return new Token(TokenKind.NewLine, "\n");
        }

        Token ParseColon()
        {
            if (Text[pos] != ':')
                return null;
            pos++;
            return new Token(TokenKind.Colon, ":");
        }

        Token ParseSymbol()
        {
            string[] symbols = {",", "+", "-", "*", "/", "(", ")", "=", "<", ">", "<>", ";", "#"};

            var text = Text.Substring(pos);
            var match = symbols
                .Where(x => text.StartsWith(x, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.Length)
                .FirstOrDefault();

            if (match == null)
                return null;

            pos += match.Length;

            return new Token(TokenKind.Symbol, match);
        }

        static readonly string[] keywords =
        {
            "to", "then", "end", "for", "next", "data", "input#", "input", "dim", "read", "let", "goto", "run", "if", "restore",
            "gosub", "return", "rem",
            "stop", "on", "wait", "load", "save", "verify", "def", "poke", "print#", "print", "cont", "list", "clr", "cmd", "sys",
            "open", "close", "get", "new",
            "fn", "not", "step", "and", "or", "sgn", "abs", "usr", "fre", "pos", "sqr", "rnd", "log", "exp", "cos", "sin", "tan", "atn",
            "peek", "len", "str$", "val",
            "asc", "chr$", "left$", "right$", "mid$",
        };

        string GetKeywordAt(int position)
        {
            var text = Text.Substring(position);
            var match = keywords
                .Where(x => text.StartsWith(x, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.Length)
                .FirstOrDefault();

            return match;
        }

        Token ParseKeyword()
        {
            var match = GetKeywordAt(pos);

            if (match == null)
                return null;

            Token result;
            if (match == "rem")
            {
                var posNewline = Text.IndexOf('\n', pos);
                var content = Text.Substring(pos + 3, posNewline - pos - 3).Trim();
                var commentTextExcludingNewline = $"rem {content}";
                result = new Token(TokenKind.Keyword, commentTextExcludingNewline);
                pos = posNewline;
            }
            else
            {
                result = new Token(TokenKind.Keyword, match);
                pos += match.Length;
            }

            return result;
        }
    }
}
