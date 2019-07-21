using System;
using System.Collections.Generic;
using System.Linq;

namespace CommodoreBasicReformatter
{
    /*
     * Grammar
     * PROGRAM ::= ( NEWLINE | LINE )*
     * LINE    ::= DIGIT STMTS NEWLINE
     * STMTS   ::= STMT ( COLON STMT )* 
     * STMT    ::= ( KEYWORD | DIGIT | STRING | SYMBOL )+
     * KEYWORD ::= 'to' | 'then' | 'end' | 'for' | 'next' | 'data' | 'dim' | 'read' | 'let' | 'goto' | ...
     * SYMBOL  ::= ',' | '+' | '-' | '*' | '/' | '(' | ')' | '=' | '<' | '>' | '<>' | ';' | '#'
     * STRING  ::= 'a'..'z' ( 'a'..'z' | '0'..'9' | '%' | '$' )*
     * DIGIT   ::= ( 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 )+
     * NEWLINE ::= '\n'
     * COLON   ::= ':'
     */
    public class Grammar
    {
        List<Token> tokens;
        int pos;

        public List<GrammarLine> Parse(string text)
        {
            var t = new Tokenizer(text);
            tokens = t.ReadAll();

            var result = new List<GrammarLine>();

            while (!Peek(TokenKind.EOF))
            {
                result.Add(ParseLine());
            }

            Eat(TokenKind.EOF);

            return result;
        }

        Token Eat(TokenKind token)
        {
            if (Peek(token))
                return tokens[pos++];

            throw new Exception($"Expecting '{token}' at pos {pos}. Token is '{tokens[pos]}'");
        }

        GrammarLine ParseLine()
        {
            var lineNo = Eat(TokenKind.Digit);

            var content = new List<GrammarStmt>();
            content.Add(ParseStmt());

            while (Peek(TokenKind.Colon))
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
            if (IsEof())
                return type == TokenKind.EOF;

            return tokens[pos].Type == type;
        }

        GrammarStmt ParseStmt()
        {
            var content = tokens
                .Skip(pos)
                .TakeWhile(x => x.Type != TokenKind.NewLine && x.Type != TokenKind.Colon)
                .ToList();

            pos += content.Count;

            return new GrammarStmt(content);
        }
    }

    public class GrammarLine
    {
        public readonly int LineNumber;
        public readonly List<GrammarStmt> Stmts;

        public GrammarLine(int linenumber, List<GrammarStmt> stmts)
        {
            LineNumber = linenumber;
            Stmts = stmts;
        }
    }

    public class GrammarStmt
    {
        public readonly List<Token> Content;

        public GrammarStmt(List<Token> content)
        {
            Content = content;
        }
    }
}