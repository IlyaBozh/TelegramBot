using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        User(string id, string name, string userName)
        {
            Id = id;
            Name = name;
            UserName = userName;
        }
    }
}
