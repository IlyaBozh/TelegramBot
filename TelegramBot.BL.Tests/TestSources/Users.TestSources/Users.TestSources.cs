using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Tests.TestSources.Users.TestSources
{
    public class UsersTestSources : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            string newName = "Крош";
            User actualUser = new User(123456, "Бараш", "Бараш123");
            User expectedUser = new User(123456, "Крош", "Бараш123");
            yield return new object[] { newName, actualUser, expectedUser };

            newName = "123";
            actualUser = new User(123456, "Бараш", "Бараш123");
            expectedUser = new User(123456, "123", "Бараш123");
            yield return new object[] { newName, actualUser, expectedUser };

            newName = "кРаSaVchiK_098";
            actualUser = new User(123456, "Бараш", "Бараш123");
            expectedUser = new User(123456, "кРаSaVchiK_098", "Бараш123");
            yield return new object[] { newName, actualUser, expectedUser };

        }
    }

    public class UsersNegativeTestSources : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            string newName = " ";
            User actualUser = new User(123456, "Бараш", "Бараш123");
            yield return new object[] { newName, actualUser};

            newName = "";
            actualUser = new User(123456, "Бараш", "Бараш123");
            yield return new object[] { newName, actualUser };
        }
    }
}
