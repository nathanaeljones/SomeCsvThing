using System;
using System.Collections.Generic;
using System.Text;

namespace NathanaelInterview.Internal
{
    public class Tokenizer
    {

        public Tokenizer(string input)
        {
            remainder = input;
            parsed = new List<Token>();
        }
        protected string remainder;
        protected int location = 0;
        protected List<Token> parsed;
        
        protected string PopChars(int count){
            location += count;
            var result = remainder.Substring(0,count);
            remainder = remainder.Remove(0,count);
            return result;
        }
        protected Token NextToken()
        {
            if (remainder.Length == 0) return null; //We hit EOF

            //Parse column separator tokens
            if (remainder[0] == ',') {
                return new Token() { Type = TokenType.COL_SEP, Value = PopChars(1), Location=location};
            }

            //Parse row separator tokens
            if (remainder.Length > 1 && remainder[0] == '\r' && remainder[1] == '\n'){
                return new Token() { Type = TokenType.ROW_SEP, Value = PopChars(2), Location = location};
            }

            //Parse escaped fields
            var t = ParseEscapedField();
            if (t != null) return t;

            //Parse textdata tokens
            int count = 0;
            foreach(char c in remainder){
                if (c == '\"' || c == '\r' || c == '\n' || c == ',' || c < 0x20){
                    break;
                }else{
                    count++;
                }
            }
            if (count > 0){
                return new Token(){ Type = TokenType.FIELD, Value = PopChars(count), Location = location};
            }

            //None of our tokenization rules matched
            throw new Exception("Unrecognized token - \\x" + (int)remainder[0]);
        }

        protected Token ParseEscapedField(){
            if (remainder[0] != '\"') return null;

            var field = new StringBuilder();

            int i = 1;
            bool completed = false;
            while (i < remainder.Length){
                var c = remainder[i];

                //Decode double quotes
                if (i < remainder.Length -1){
                    var nextc = remainder[i + 1];
                    if (c == '\"' && nextc == '\"'){
                        i+=2;
                        field.Append('\"');
                        continue;
                    }
                }
                //Look for closing quote
                if (c == '\"'){
                    i++; 
                    completed = true;
                    break; 
                }
                //Append non-quote characters (we're OK with characters below 20, such as newlines etc).
                field.Append(c);
                i++;
            }

            var start = location;
            remainder = remainder.Remove(0, i);
            location -= i;
            if (!completed) throw new Exception("Escaped field never closed - reached end of document first.");
            return new Token(){Type= TokenType.ESCAPED_FIELD, Value = field.ToString(), Location = start};
        }

        public Token[] ParseAllTokens()
        {
            if (parsed.Count > 0) return parsed.ToArray(); //Don't reparse

            //Collect all tokens
            Token t;
            while ((t = NextToken()) != null) parsed.Add(t);
            return parsed.ToArray();
        }

    }
}
