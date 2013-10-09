using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Tests
{
    public class AssertDeep
    {
        public static void AreEqual(object expected, object actual)
        {
            var expectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(expected);
            var actualJson = Newtonsoft.Json.JsonConvert.SerializeObject(actual);
            
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
