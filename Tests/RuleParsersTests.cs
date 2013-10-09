using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NathanaelInterview;
using NathanaelInterview.Internal;

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

            AssertDeep.AreEqual(result, new FieldParseResult()
            {
                Length = 3,
                Value = "foo"
            });
        }

        [Test]
        public void CanParseNonescapedFields2()
        {
            var tokens = new Tokenizer().Tokenize("foo,\"bar,\",");

            var result = new RuleParsers().ParseField(tokens);

            AssertDeep.AreEqual(result, new FieldParseResult()
            {
                Length = 3,
                Value = "foo"
            });
        }
        
        [Test]
        public void CanParseEscapedFields()
        {
            var tokens = new Tokenizer().Tokenize("\"foo\",");

            var result = new RuleParsers().ParseField(tokens);

            AssertDeep.AreEqual(result, new FieldParseResult()
            {
                Length = 5,
                Value = "foo"
            });
        }

        [Test]
        public void CanParseComplexEscapedFields()
        {
            var tokens = new Tokenizer().Tokenize("\"foo\"\",\r\n\",");

            var result = new RuleParsers().ParseField(tokens);

            AssertDeep.AreEqual(result, new FieldParseResult()
            {
                Length = 10,
                Value = "foo\",\r\n"
            });
        }

        [Test]
        public void CanParseEmptyRecord()
        {
            var tokens = new Tokenizer().Tokenize("");

            var result = new RuleParsers().ParseRecord(tokens);

            AssertDeep.AreEqual(result, new RecordParseResult()
            {
                Length = 0,
                Values = new[] {""}
            });
        }

        [Test]
        public void CanParseRecord()
        {
            var tokens = new Tokenizer().Tokenize("foo,\"bar,\",");

            var result = new RuleParsers().ParseRecord(tokens);

            AssertDeep.AreEqual(result, new RecordParseResult()
            {
                Length = 11,
                Values = new[] { "foo", "bar,", ""}
            });
        }

        [Test]
        public void CanParseEmptyFile()
        {
            var tokens = new Tokenizer().Tokenize("");

            var result = new RuleParsers().ParseFile(tokens);

            AssertDeep.AreEqual(result, new FileParseResult()
            {
                Length = 0,
                Values = new[] { new[] { "" } }
            });
        }

        [Test]
        public void CanParseFile()
        {
            var tokens = new Tokenizer().Tokenize("foo,bar\r\nbaz\r\n");

            var result = new RuleParsers().ParseFile(tokens);

            AssertDeep.AreEqual(result, new FileParseResult()
            {
                Length = tokens.Length,
                Values = new [] { new [] {"foo", "bar"}, new string[] {"baz"}, new string[] {""}}
            });
        }
    }
}
