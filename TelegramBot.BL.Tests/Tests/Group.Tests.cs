using NUnit.Framework;
using TelegramBot.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests.TestsSources;

namespace TelegramBot.BL.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(AddUserTestSource))]
        public void AddUserTest(User name, Group newGroup, Group expectedGroup)
        {
            Group actualGroup = newGroup;
            actualGroup.AddUser(name);

            Assert.AreEqual(expectedGroup, actualGroup);
        }

       
    }
}