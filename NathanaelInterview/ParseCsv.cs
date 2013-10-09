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
            var tokens = new Tokenizer().Tokenize(input);

            var fileResult = new RuleParsers().ParseFile(tokens);

            return fileResult.Values;
        }
    }
}
