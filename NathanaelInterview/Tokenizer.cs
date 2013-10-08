using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NathanaelInterview
{
    public class Tokenizer
    {
        public Token[] Tokenize(string input)
        {
            List<Token> result = new List<Token>();

            foreach(var chr in input)
            {
                Token nextToken;

                switch (chr)
                {
                    case ',':
                        nextToken = new Token()
                        {
                            Type = TokenType.COMMA,
                            Value = ','
                        };
                        break;
                    case '\r':
                        nextToken = new Token()
                        {
                            Type = TokenType.CR,
                            Value = '\r'
                        };
                        break;
                    case '\n':
                        nextToken = new Token()
                        {
                            Type = TokenType.LF,
                            Value = '\n'
                        };
                        break;
                    case '"':
                        nextToken = new Token()
                        {
                            Type = TokenType.DQUOTE,
                            Value = '"'
                        };
                        break;
                    default:
                        if (chr < 0x20)
                            throw new Exception("Unrecognized token - \\x" + (int)chr);

                        nextToken = new Token()
                        {
                            Type = TokenType.TEXTDATA,
                            Value = chr
                        };
                        break;
                }

                result.Add(nextToken);
            }

            return result.ToArray();
        }
    }
}
