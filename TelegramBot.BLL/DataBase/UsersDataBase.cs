using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.DataBase
{
    public class UsersDataBase
    {
        public List<Group> UserGroups { get; set; }
        private static UsersDataBase _instance;

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
    }
}
