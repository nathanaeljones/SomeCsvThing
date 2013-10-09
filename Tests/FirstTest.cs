using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NathanaelInterview;

namespace Tests
{
    public class FirstTest
    {
        [Test]
        public void ParseCsv()
        {
            var input = "foo";
            var firstRow = new string[] {"foo"}; 
            var expectedResult = new string[][]
            {
                firstRow
            };

            var result = new ParseCsv().Parse(input);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ParseCsvWithNewlines()
        {
            var input = "foo\r\nbar\r\nbaz";
            var expectedResult = new string[][]
            {
                new string[] {"foo"}, 
                new string[] {"bar"}, 
                new string[] {"baz"}
            };

            var result = new ParseCsv().Parse(input);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ParseCsvWithNewlines2()
        {
            var input = "field1,field2,field3\r\n" +
                "\"aaa\r\n\",\"bb,b\",\"ccc\"\r\n" + 
                "\"in \"\"quotes\"\"\",2,3\r\n" + 
                "1,2,\r\n" + 
                "zzz,yyy,xxx\r\n" + 
                "1,,3\r\n" + 
                ",,";
            var expectedResult = new string[][]{
                 new string[]{"field1", "field2", "field3"},
                 new string[]{ "aaa\r\n", "bb,b", "ccc"},
                 new string[]{"in \"quotes\"", "2", "3"},
                 new string[]{"1", "2", ""},
                 new string[]{"zzz", "yyy", "xxx"},
                 new string[]{"1", "", "3"},
                 new string[]{"", "", ""}};

            var result = new ParseCsv().Parse(input);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
