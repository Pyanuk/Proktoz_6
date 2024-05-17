using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace Calen
{
    internal class Filecs  
    {
        public static List<T> Deserialization<T>(string filePath)
        {
            List<T> result = new List<T>();
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var serializer = new JsonSerializer();
                    result = (List<T>)serializer.Deserialize(reader, typeof(List<T>));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing file: {ex.Message}");
            }
            return result;
        }


        public static void Serialization<T>(List<T> list, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, list);
            }
        }
    }
}
