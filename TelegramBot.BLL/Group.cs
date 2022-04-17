using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class Group
    {

        public List<User> UserGroups { get; set; }

        public string Name { get; set; }

        Group(string name, List<User> userGroups)
        {

            Name = name;
            UserGroups = userGroups;
        }

        public void AddUser (User user)
        {
            UserGroups.Add(user);
        }

        public void DeleteUser(User id)
        {
            UserGroups.Remove(id);
        }
    }
}
