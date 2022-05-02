using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace TelegramBot.BL.DataBase
{
    public class UsersDataBase
    {
        public List<Group> UserGroups { get; set; }
        private static UsersDataBase _instance;
        private const string filePath = @"usergroups.data"; // добавить в wpf

        private UsersDataBase()
        {
            Group RestOfAll = new Group("пользователи без группы");
            UserGroups = new List<Group>();
            UserGroups.Add(RestOfAll);
        }

        public static UsersDataBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UsersDataBase();
            }
            return _instance;
        }

        public string Serialize(List<Group> UserGroups)
        {
            return JsonSerializer.Serialize<List<Group>>(UserGroups);
        }

        public List<Group> Deserialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                return JsonSerializer.Deserialize<List<Group>>(json);
            }
        }

        public void Save(List<Group> UserGroups)
        {
            string json = Serialize(UserGroups);
            using(StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(json);
            }
        }

        public List<Group> Load()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadLine();
                return Deserialize(json);
            }
        }
    }
}
