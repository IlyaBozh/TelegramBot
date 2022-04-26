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

        //User(long id, string name, string userName)
        //{
        //    Id = id;
        //    Name = name;
        //    UserName = userName;
        //}

        public User(string firstName, string lastName, long id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            Id = id;
        }

        public void ChangeName(string newName)
        {
            if (newName != "" && newName != " ")
            {
                Name = newName;
            }          
        }
    }
}
