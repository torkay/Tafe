using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GroupForge
{
    public static class FileManager
    {
        public static void SaveToFile(List<Data.TeamMember> members, string filePath)
        {
            string json = JsonConvert.SerializeObject(members);
            File.WriteAllText(filePath, json);
        }

        public static List<Data.TeamMember> ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Data.TeamMember>>(json);
            }
            else
            {
                return new List<Data.TeamMember>();
            }
        }
    }
}
