using NUnit.Framework;
using TelegramBot.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests.TestSources;

namespace TelegramBot.BL.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(AddUserTestSource))]
        public void AddUserTest(User newUser, Group newGroup, Group expectedGroup)
        {
            Group actualGroup = newGroup;
            actualGroup.AddUser(newUser);

            Assert.AreEqual(expectedGroup, actualGroup);
        }
        [TestCaseSource(typeof(AddUserNegativeTestSource))]
        public void AddUserTest_WhenNewUserIsNull_ShouldThrowArgumentNullException(User newUser, Group newGroup)
        {
            Assert.Throws<ArgumentNullException>(() => newGroup.AddUser(newUser));
        }

        [TestCaseSource(typeof(DeleteUserTestSource))]
        public void DeleteUserTest(User newUser, Group newGroup, Group expectedGroup)
        {
            Group actualGroup = newGroup;
            actualGroup.DeleteUser(newUser);

            Assert.AreEqual(expectedGroup, actualGroup);
        }
        [TestCaseSource(typeof(DeleteUserByIdTestSource))]
        public void DeleteUserByIdTest(long id, Group newGroup, Group expectedGroup)
        {
            Group actualGroup = newGroup;
            actualGroup.DeleteUserById(id);

            Assert.AreEqual(expectedGroup, actualGroup);
        }
    }
}