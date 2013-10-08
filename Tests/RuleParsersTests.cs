using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NathanaelInterview;

namespace Tests
{
    public class RuleParsersTests
    {
        [Test]
        public void FailsGracefullyOnPartialQuotedField()
        {
            var tokens = new Tokenizer().Tokenize("\"foo");

            var result = new RuleParsers().ParseField(tokens);

            Assert.Null(result);
        }

        [Test]
        public void CanParseNonescapedFields()
        {
            var tokens = new Tokenizer().Tokenize("foo,");

            var result = new RuleParsers().ParseField(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result.Value, "foo");
        }

        [Test]
        public void CanParseNonescapedFields2()
        {
            var tokens = new Tokenizer().Tokenize("foo,\"bar,\",");

            var result = new RuleParsers().ParseField(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("foo", result.Value);
        }
        
        [Test]
        public void CanParseEscapedFields()
        {
            var tokens = new Tokenizer().Tokenize("\"foo\",");

            var result = new RuleParsers().ParseField(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result.Value, "foo");
        }

        [Test]
        public void CanParseComplexEscapedFields()
        {
            var tokens = new Tokenizer().Tokenize("\"foo\"\",\r\n\",");

            var result = new RuleParsers().ParseField(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(10, result.Length);
            Assert.AreEqual("foo\",\r\n", result.Value);
        }

        [Test]
        public void CanParseEmptyRecord()
        {
            var tokens = new Tokenizer().Tokenize("");

            var result = new RuleParsers().ParseRecord(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Length);
            Assert.AreEqual(1, result.Values.Length);
            Assert.AreEqual("", result.Values[0]);
        }

        [Test]
        public void CanParseRecord()
        {
            var tokens = new Tokenizer().Tokenize("foo,\"bar,\",");

            var result = new RuleParsers().ParseRecord(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(11, result.Length);
            Assert.AreEqual(3, result.Values.Length);
            Assert.AreEqual("foo", result.Values[0]);
            Assert.AreEqual("bar,", result.Values[1]);
            Assert.AreEqual("", result.Values[2]);
        }

        [Test]
        public void CanParseEmptyFile()
        {
            var tokens = new Tokenizer().Tokenize("");

            var result = new RuleParsers().ParseFile(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Length);
            Assert.AreEqual(1, result.Values.Length);
            Assert.AreEqual(1, result.Values[0].Length);
            Assert.AreEqual("", result.Values[0][0]);
        }

        [Test]
        public void CanParseFile()
        {
            var tokens = new Tokenizer().Tokenize("foo,bar\r\nbaz\r\n");

            var result = new RuleParsers().ParseFile(tokens);

            Assert.NotNull(result);
            Assert.AreEqual(tokens.Length, result.Length);
            Assert.AreEqual(3, result.Values.Length);
            Assert.AreEqual(2, result.Values[0].Length);
            Assert.AreEqual(new []{"foo", "bar"}, result.Values[0]);
            Assert.AreEqual(1, result.Values[1].Length);
            Assert.AreEqual(new[] { "baz" }, result.Values[1]);
            Assert.AreEqual(new[] { "" }, result.Values[2]);
        }
    }
}
