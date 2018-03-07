using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace TestJsonToExpando.Test
{
    [TestClass]
    public class UnitTestExpandoJson
    {
        [TestMethod]
        public void TestJObject()
        {
            var sample = Sample.Create();
            var json = JsonConvert.SerializeObject(sample);

            var obj = JsonConvert.DeserializeObject(json);
            var jObj = obj as JObject;
            Assert.IsNotNull(jObj);

            var jTokenA = jObj["A"];
            var jArray = jTokenA as JArray;
            Assert.IsNotNull(jArray);

            var jToken0 = jArray[0];
            Assert.IsNotNull(jToken0);
            var valueOf0 = jToken0.Value<long>();
            Assert.AreEqual(sample.A[0], valueOf0);

            var jTokenB = jObj["B"];
            Assert.IsNotNull(jTokenB);
            var valueOfB = jTokenB.Value<bool>();
            Assert.AreEqual(sample.B, valueOfB);

            var jTokenC = jObj["C"];
            var jObjC = jTokenC as JObject;
            Assert.IsNotNull(jObjC);
        }

        [TestMethod]
        public void TestJObjectDynamic()
        {
            var sample = Sample.Create();
            var json = JsonConvert.SerializeObject(sample);

            dynamic obj = JsonConvert.DeserializeObject(json);
            Assert.IsInstanceOfType(obj, typeof(JObject));

            var a = obj.A;
            Assert.IsInstanceOfType(a, typeof(JArray));

            var a0 = a[0];
            Assert.IsInstanceOfType(a0, typeof(JValue));
            Assert.AreEqual(sample.A[0], a0.Value); // Ewww!

            var b = obj.B;
            Assert.IsInstanceOfType(b, typeof(JValue));
            Assert.AreEqual(sample.B, b.Value); // Yuck!

            var c = obj.C;
            Assert.IsInstanceOfType(c, typeof(JObject));
        }

        [TestMethod]
        public void TestExpandoObject()
        {
            var sample = Sample.Create();
            var json = JsonConvert.SerializeObject(sample);

            var converter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(json, converter);
            Assert.IsInstanceOfType(obj, typeof(ExpandoObject));

            var a = obj.A;
            Assert.IsInstanceOfType(a, typeof(List<object>));

            var a0 = a[0];
            Assert.IsInstanceOfType(a0, typeof(long));
            Assert.AreEqual(sample.A[0], a0); // No call to a0.Value!

            var b = obj.B;
            Assert.IsInstanceOfType(b, typeof(bool));
            Assert.AreEqual(sample.B, b); // Woo hoo!

            var c = obj.C;
            Assert.IsInstanceOfType(c, typeof(ExpandoObject));
        }

        private string[] _sourceLists = new string[] { "sample.json", "sample1.json" };

        [TestMethod, DataSource("_sourceLists")]
        public void TestJsonData(string fileName)
        {
            string json = ReadJsonFile.GetJsonFileContentAsString(fileName);
            dynamic obj = JsonConvert.DeserializeObject(json);
            Assert.IsInstanceOfType(obj, typeof(JObject));
        }

    }
}
