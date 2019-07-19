using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommodoreBasicReformatter
{
    class Grammar
    {
        readonly List<Token> tokens;
        int pos;

        public Grammar(string text)
        {
            var t = new Tokenizer(text);
            tokens = t.ReadAll();
        }

        public List<GrammarLine> Parse()
        {
            var result = new List<GrammarLine>();

            while (!Peek(TokenKind.EOF))
            {
                result.Add(ParseLine());
            }

            Eat(TokenKind.EOF);

            return result;
        }

        void Eat(TokenKind token)
        {
            if (Peek(token))
            {
                pos++;
                return;
            }

            throw new Exception($"Expecting '{token}' at pos {pos}");
        }

        Token Digit()
        {
            if (Peek(TokenKind.Digit))
                return tokens[pos++];

            throw new Exception($"Expected digit at token {pos} token is '{tokens[pos]}'");
        }

        GrammarLine ParseLine()
        {
            var lineNo = Digit();

            var content = new List<GramarStmt>();
            content.Add(ParseStmt());

            while (!IsEof() && Peek(TokenKind.Colon))
            {
                Eat(TokenKind.Colon);
                content.Add(ParseStmt());
            }

            Eat(TokenKind.NewLine);

            return new GrammarLine(int.Parse(lineNo.Value), content);
        }

        bool IsEof()
        {
            return pos == tokens.Count;
        }

        bool Peek(TokenKind type)
        {
            return tokens[pos].Type == type;
        }

        GramarStmt ParseStmt()
        {
            var content = tokens
                .Skip(pos)
                .TakeWhile(x => x.Type != TokenKind.NewLine && x.Type != TokenKind.Colon)
                .ToList();

            pos += content.Count;

            return new GramarStmt(content);
        }
    }

    class GrammarLine
    {
        public readonly int LineNumber;
        public readonly List<GramarStmt> Stmts;

        public GrammarLine(int linenumber, List<GramarStmt> stmts)
        {
            LineNumber = linenumber;
            Stmts = stmts;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            return $"{LineNumber} {string.Join(" : ", Stmts.Select(x => x.ToString()))}";
        }
    }

    class GramarStmt
    {
        public readonly List<Token> Content;

        public GramarStmt(List<Token> content)
        {
            Content = content;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Content.Count - 1; i++)
            {
                var current = Content[i];
                sb.Append(current.Value);

                if (current.Type == TokenKind.Symbol && current.Value != "=")
                    continue;

                var next = Content[i + 1];
                if (next.Type != TokenKind.Symbol || next.Value == "=")
                    sb.Append(" ");
            }

            sb.Append(Content.Last().Value);

            return sb.ToString();
        }
    }
}