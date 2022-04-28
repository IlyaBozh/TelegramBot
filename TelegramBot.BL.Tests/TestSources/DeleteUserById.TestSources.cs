using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL;
using TelegramBot.BL.Tests;

namespace TelegramBot.BL.Tests.TestSources
{
    public class DeleteUserByIdTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<User> TestUsersGroupOne = new List<User>()
            {
                new User(123456781, "Valera", "Val123"),
                new User(123456782, "Kesha", "Kesha123"),
                new User(123456783, "Nusha", "Nusha123"),
            };
            List<User> TestUsersGroupTwo = new List<User>()
            {
                new User(123456781, "Valera", "Val123"),
                new User(123456782, "Kesha", "Kesha123"),
                new User(123456783, "Nusha", "Nusha123"),
            };
            List<User> TestUsersGroupThree = new List<User>()
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
            List<User> SecondNewUsersGroup = new List<User>()
            {
                new User(123456782, "Kesha", "Kesha123"),
                new User(123456783, "Nusha", "Nusha123"),
            };
            List<User> ThirdNewUsersGroup = new List<User>()
            {
                new User(123456781, "Valera", "Val123"),
                new User(123456782, "Kesha", "Kesha123"),
            };


            long id = 123456782;
            Group actualGroup = new Group("Красавчики", TestUsersGroupOne);
            Group expectedGroup = new Group("Красавчики", newUsersGroup);
            yield return new object[] {id, actualGroup, expectedGroup };

            id = 123456781;
            actualGroup = new Group("Красавчики", TestUsersGroupTwo);
            expectedGroup = new Group("Красавчики", SecondNewUsersGroup);
            yield return new object[] { id, actualGroup, expectedGroup };

            id = 123456783;
            actualGroup = new Group("Красавчики", TestUsersGroupThree);
            expectedGroup = new Group("Красавчики", ThirdNewUsersGroup);
            yield return new object[] { id, actualGroup, expectedGroup };


        }
    }
}
