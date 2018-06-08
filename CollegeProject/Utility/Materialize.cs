using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CollegeProject.Utility
{
    public static class Materialize
    {
        public static T DeserializeFile<T>(string filename)
        {
            return File.Exists(filename) ? JsonConvert.DeserializeObject<T>(File.ReadAllText(filename)) : default(T);
        }

        public static void SerializeFile<T>(string filename, T value)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(value));
        }
    }
}
