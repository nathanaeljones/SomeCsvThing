using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NathanaelInterview.Internal
{
    public class RuleParsers
    {
        public FieldParseResult ParseField(Token[] tokens)
        {
            if (tokens.Length > 0 && tokens[0].Type == TokenType.DQUOTE)
            {
                StringBuilder result = new StringBuilder();
                var position = 1;

                var keepParsing = true;
                while (keepParsing && tokens.Length > position)
                {
                    switch (tokens[position].Type)
                    {
                        case TokenType.TEXTDATA:
                        case TokenType.COMMA:
                        case TokenType.CR:
                        case TokenType.LF:
                            result.Append(tokens[position].Value);
                            position++;
                            break;

                        case TokenType.DQUOTE:
                            if (tokens.Length > position + 1 && tokens[position + 1].Type == TokenType.DQUOTE)
                            {
                                result.Append("\"");
                                position += 2;
                            }
                            else
                            {
                                keepParsing = false;
                            }
                            break;
                        default:
                            keepParsing = false;
                            break;
                    }
                }

                if (tokens.Length > position && tokens[position].Type == TokenType.DQUOTE)
                {
                    return new FieldParseResult()
                    {
                        Length = position + 1,
                        Value = result.ToString()
                    };
                }
            }
            else
            {
                StringBuilder result = new StringBuilder();

                foreach (var token in tokens)
                {
                    if (token.Type == TokenType.TEXTDATA)
                        result.Append(token.Value);
                    else
                        break;
                }

                return new FieldParseResult()
                {
                    Length = result.Length,
                    Value = result.ToString()
                };
            }

            return null;
        }

        public RecordParseResult ParseRecord(Token[] tokens)
        {
            var position = 0;
            List<string> result = new List<string>();

            var firstField = ParseField(tokens);
            if (firstField == null)
                return null;

            result.Add(firstField.Value);

            position += firstField.Length;

            while (position < tokens.Length && tokens[position].Type == TokenType.COMMA)
            {
                var nextField = ParseField(tokens.Skip(position + 1).ToArray());

                if (nextField == null)
                    break;

                result.Add(nextField.Value);
                position += 1 + nextField.Length;
            }

            return new RecordParseResult()
            {
                Values = result.ToArray(),
                Length = position
            };
        }

        public FileParseResult ParseFile(Token[] tokens)
        {
            var position = 0;
            List<string[]> results = new List<string[]>();

            var firstRecord = ParseRecord(tokens);
            if (firstRecord == null)
                return null;

            position += firstRecord.Length;
            results.Add(firstRecord.Values);

            while (tokens.Length > position + 2 
                && tokens[position].Type == TokenType.CR 
                && tokens[position + 1].Type == TokenType.LF)
            {
                var nextRecord = ParseRecord(tokens.Skip(position + 2).ToArray());

                if (nextRecord == null)
                    break;

                position += nextRecord.Length + 2;
                results.Add(nextRecord.Values);
            }

            if (tokens.Length > position + 1
                && tokens[position].Type == TokenType.CR
                && tokens[position + 1].Type == TokenType.LF)
            {
                position += 2;
            }
            
            return new FileParseResult()
            {
                Length = position,
                Values = results.ToArray()
            };
        }
    }
}
