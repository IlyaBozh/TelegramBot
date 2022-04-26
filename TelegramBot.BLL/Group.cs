using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL
{
    public class Group
    {
        public List<User> UserGroups { get; set; }

        public string NameGroup { get; set; }




        public Group(string name)
        {
            NameGroup = name;
            UserGroups=new List<User>();
        }

        public Group(string name, List<User> userGroups)
        {
            NameGroup = name;
            UserGroups = userGroups;
        }

        public void AddUser(User user)
        {
            UserGroups.Add(user);
        }

        public void DeleteUser(User name)
        {
            UserGroups.Remove(name);
        }

        //public void DeleteUserById(User id)
        //{
        //    UserGroups.Remove(id);
        //}

        public void Edit(string name, string newName)
        {
            if (newName != "" && newName != " ")
            {
                NameGroup = newName;
            }
        }
    }
}
