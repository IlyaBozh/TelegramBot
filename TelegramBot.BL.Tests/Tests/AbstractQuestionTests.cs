using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL;
using TelegramBot.BL.Questions;
using TelegramBot.BL.Tests.TestSources;


namespace TelegramBot.BL.Tests
{
    public class AbstractQuestionTests
    {
        [TestCaseSource(typeof(EditDiscriptionTestSource))]
        public void EditDiscriptionTest(string newDescription, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.EditDiscription(newDescription);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }

    }
}
