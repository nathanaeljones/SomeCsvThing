namespace NathanaelInterview.Internal
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
