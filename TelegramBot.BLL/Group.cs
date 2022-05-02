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
        public void AddUser(User newUser)
        {
            if(newUser is null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            UserGroups.Add(newUser);
        }
        public void DeleteUser(User newUser)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            UserGroups.RemoveAll(User => User.Name == newUser.Name);
        }
        public void DeleteUserById(long id)
        {
            foreach (User user in UserGroups)
            {
                if (id == user.Id)
                {
                    UserGroups.Remove(user);
                    break;
                }
            }
        }

        public void Edit(string newName)
        {
            if (newName == null)
            {
                throw new ArgumentNullException(nameof(newName));
            }
            NameGroup = newName;
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
            string tmp = $"{NameGroup}: ";

            for (int i = 0; i < UserGroups.Count; i++)
            {
                tmp += $"[{UserGroups[i].Name}; {UserGroups[i].Id}; {UserGroups[i].UserName}]";
            }

            return tmp;
        }
    }
}
