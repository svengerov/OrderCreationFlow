using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infra.Helpers
{
    public static class SerializerHelper
    {
        public static string SerializeObjToJson(object obj)
        {
            string jsonText = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return jsonText;
        }
        public static void SerializeObjToJsonFile(string targetFilePath, object obj)
        {
            string jsonString = SerializeObjToJson(obj);
            File.WriteAllText(targetFilePath, jsonString);
        }

        public static T DeserializeJsonFile<T>(string filePath) where T : class
        {
            string jsonText = File.ReadAllText(filePath);
            T obj = JsonConvert.DeserializeObject<T>(jsonText);
            return obj;
        }
    }
}
