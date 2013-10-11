using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NathanaelInterview.Internal;

namespace NathanaelInterview
{
    public class ParseCsv
    {
        public string[][] Parse(string input)
        {
            return Parse(new Tokenizer(input).ParseAllTokens());
        }

        protected string[][] Parse(Token[] tokens)
        {
            List<string[]> table = new List<string[]>();
            List<string> row = new List<string>();

            Token prevToken = null;
            Token endToken = tokens.Length > 0 ? tokens[tokens.Length - 1] : null;
            foreach (Token t in tokens)
            {
                if (t.Type == TokenType.ESCAPED_FIELD || t.Type == TokenType.FIELD)
                {
                    if (prevToken != null && prevToken.Type != TokenType.ROW_SEP && prevToken.Type != TokenType.COL_SEP) throw new Exception("Entire field must be escaped");
                    row.Add(t.Value);
                }

                //Ensure that empty fields are added.
                if ((t.Type == TokenType.COL_SEP || t.Type == TokenType.ROW_SEP) && (prevToken == null || prevToken.Type == TokenType.COL_SEP || prevToken.Type == TokenType.ROW_SEP)) row.Add("");

                //When the file ends in a comma
                if (t == endToken && t.Type == TokenType.COL_SEP) row.Add(""); 

                //Commit rows
                if (t.Type == TokenType.ROW_SEP || t == endToken)
                {
                    table.Add(row.ToArray());
                    row = new List<string>();
                }
                prevToken = t;
            }
            return table.ToArray();
        }
    }
}
