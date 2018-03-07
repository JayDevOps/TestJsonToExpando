using System.IO;
using System.Reflection;

namespace TestJsonToExpando
{
    public class ReadJsonFile
    {
        public static string GetJsonFileContentAsString(string resourceName = "sample.json")
        {
            return (new StreamReader(typeof(ReadJsonFile).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName))).ReadToEnd();
        }
    }
}
