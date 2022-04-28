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
    public class EditTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<User> users = new List<User>();

            string name = "умнички";
            Group actualGroup = new Group("Красавчики", users);
            Group expectedGroup = new Group("умнички", users);
            yield return new object[] { name, actualGroup, expectedGroup };

            name = "12345";
            actualGroup = new Group("Красавчики", users);
            expectedGroup = new Group("12345", users);
            yield return new object[] { name, actualGroup, expectedGroup };



        }
    }
}
