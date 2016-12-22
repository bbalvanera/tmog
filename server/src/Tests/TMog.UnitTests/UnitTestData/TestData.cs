using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMog.Entities;

namespace TMog.UnitTests.UnitTestData
{
    public static class TestData
    {
        public static List<Set> GetTMogSets()
        {
            using (var reader = new StreamReader("UnitTestData\\TMogSets.json"))
            {
                var json = reader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Set>>(json);

                return items;
            }
        }
    }
}
