using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NathanaelInterview;

namespace Tests
{
    public class TokenizerTests
    {
        [Test]
        public void CanTokenizeComma()
        {
            var input = ",";
            var expectedOutput = new[] {new Token() {Type = TokenType.COMMA, Value = ','}};

            var result = new Tokenizer().Tokenize(input);

            Assert.AreEqual(result, expectedOutput);
        }

        [Test]
        public void CanTokenizeAllTokenTypes()
        {
            var input = "\"\r\n,a";
            var expectedOutput = new[]
            {
                new Token() { Type = TokenType.DQUOTE, Value = '"' },
                new Token() { Type = TokenType.CR, Value = '\r' },
                new Token() { Type = TokenType.LF, Value = '\n' },
                new Token() { Type = TokenType.COMMA, Value = ',' },
                new Token() { Type = TokenType.TEXTDATA, Value = 'a' },
            };

            var result = new Tokenizer().Tokenize(input);

            Assert.AreEqual(result, expectedOutput);
        }
    
    }
}
