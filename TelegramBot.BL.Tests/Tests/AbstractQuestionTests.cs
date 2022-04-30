using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL;
using TelegramBot.BL.Questions;
using TelegramBot.BL.Tests.TestSources.AbstractQuestions;


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

        [TestCaseSource(typeof(EditDiscriptionNegativeTestSource))]
        public void EditDiscriptionTest_WhenNewDiscriptionIsNull_ShouldThrowArgumentNullException(string newDescription, AbstractQuestion newQuestion)
        {
            Assert.Throws<ArgumentNullException>(() => newQuestion.EditDiscription(newDescription));
        }

        [TestCaseSource(typeof)]
        public void EditVariantTest(int index, string newVariant, bool isVariant, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.EditVariant(index, newVariant, isVariant);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }

       

    }
}
