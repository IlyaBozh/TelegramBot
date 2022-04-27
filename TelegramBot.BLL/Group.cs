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
        }

        public Group(string name, List<User> userGroups)
        {
            NameGroup = name;
            UserGroups = userGroups;
        }

        public void AddUser(User name)
        {
            UserGroups.Add(name);
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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Group))
            {
                return false;
            }
            Group group = (Group)obj;
            if (group.NameGroup != NameGroup)
            {
                return false;
            }
            if (group.UserGroups.Count != UserGroups.Count)
            {
                return false;
            }
            for (int i = 0; i < UserGroups.Count; i++)
            {
                if (UserGroups[i].Name != group.UserGroups[i].Name)
                {
                    return false;
                }

                if (UserGroups[i].Id != group.UserGroups[i].Id)
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string str = $"[{NameGroup}: ";

            for (int i = 0; i < UserGroups.Count; i++)
            {
                str += $"[{UserGroups[i].Name}; {UserGroups[i].Id}; {UserGroups[i].UserName}]";
            }

            str += "]";

            return str;
        }
    }
}
