namespace NathanaelInterview.Internal
{
    public enum TokenType
    {
        DQUOTE,
        LF,
        CR,
        COMMA,
        TEXTDATA
    }

    public class Token
    {
        public TokenType Type;
        public char Value;
    }
}
