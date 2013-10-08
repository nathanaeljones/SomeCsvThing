using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NathanaelInterview
{
    public class ParseResult
    {
        public int Length;
    }

    public class FieldParseResult : ParseResult
    {
        public string Value;
    }

    public class RecordParseResult : ParseResult
    {
        public string[] Values;
    }

    public class FileParseResult : ParseResult
    {
        public string[][] Values;
    }
}
