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

        protected bool Equals(Token other)
        {
            return Type == other.Type && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type*397) ^ Value.GetHashCode();
            }
        }
    }
}
