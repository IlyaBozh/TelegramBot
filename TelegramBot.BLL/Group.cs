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

        public Group(string name, List<User> GroupUsers)
        {
            NameGroup = name;
            UserGroups = GroupUsers;
        }

        public void AddUser(User user)
        {
            UserGroups.Add(user);
        }

        public void DeleteUser(long id)
        {
            foreach(User user in UserGroups)
            {
                if(id == user.Id)
                {
                    UserGroups.Remove(user);
                    break;
                }
            }
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
