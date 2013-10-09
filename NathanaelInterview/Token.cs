using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NathanaelInterview
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
