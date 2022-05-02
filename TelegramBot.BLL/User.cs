using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL
{
    public class User
    {
        private string? firstName;
        private string? lastName;

        public long Id { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }

        public User()
        {

        }

        public User(long id, string name, string userName)
        {
            Id = id;
            Name = name;
            UserName = userName;
        }

        public User(string firstName, string lastName, long id)
        {
            Name = firstName;
            UserName = lastName;
            Id = id;
        }

        public void ChangeName(string newName)
        {
            if (newName != "" && newName != " ")
            {
                Name = newName;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is User))
            {
                return false;
            }
            User user = (User)obj;
            if (user.Id != Id)
            {
                return false;
            }
            if (user.Name != Name)
            {
                return false;
            }
            if(user.UserName != UserName)
            {
                return false;
            }
            return true;
        }
        public override string ToString()
        {
            string tmp = $"[{Id}, {Name}, {UserName}] ";
            return tmp;
        }
    }
}
