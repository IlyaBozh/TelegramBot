using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests.TestSources.Users.TestSources;

namespace TelegramBot.BL.Tests
{
    public class UsersTests
    {
        [TestCaseSource(typeof(UsersTestSources))]
        public void ChangeNameTest(string newName, User newUser, User expectedUser)
        {
            User actualUser = newUser;
            actualUser.ChangeName(newName);

            Assert.AreEqual(expectedUser, actualUser);
        }

        [TestCaseSource(typeof(UsersNegativeTestSources))]
        public void ChangeNameTest_WhenNewNameIsNull_ShouldThrowArgumentNullException(string newName, User newUser)
        {
            Assert.Throws<ArgumentNullException>(() => newUser.ChangeName(newName));
        }
    }
}
