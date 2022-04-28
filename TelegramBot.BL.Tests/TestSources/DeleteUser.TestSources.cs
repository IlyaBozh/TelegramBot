using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests;

namespace TelegramBot.BL.Tests.TestSources
{
    public class DeleteUserTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<User> UsersGroup = new List<User>()
            {
                new User(123456781, "Valera", "Val123"),
                new User(123456782, "Kesha", "Kesha123"),
                new User(123456783, "Nusha", "Nusha123"),
            };
            List<User> newUsersGroup = new List<User>()
            {
                new User(123456781, "Valera", "Val123"),
                new User(123456783, "Nusha", "Nusha123"),
            };

            User newUser = new User(123456782, "Kesha", "Kesha123");
            Group actualGroup = new Group("Красавчики", UsersGroup);
            Group expectedGroup = new Group("Красавчики", newUsersGroup);
            yield return new object[] { newUser, actualGroup, expectedGroup };

        }
    }
}
