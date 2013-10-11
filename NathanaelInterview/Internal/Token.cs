namespace NathanaelInterview.Internal
{
    public enum TokenType
    {
        ESCAPED_FIELD,
        ROW_SEP,
        COL_SEP,
        FIELD
    }

    public class Token
    {
        public TokenType Type;
        public string Value;
        public int Location;
    }
}
