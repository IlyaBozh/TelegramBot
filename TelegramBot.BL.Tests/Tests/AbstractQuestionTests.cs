using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL;
using TelegramBot.BL.Questions;
using TelegramBot.BL.Tests.TestSources.AbstractQuestions;
using TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources;


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
        } // ???
        [TestCaseSource(typeof(EditVariantTestSource))]
        public void EditVariantTest(int index, string newVariant, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.EditVariant(index, newVariant);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }
        [TestCaseSource(typeof (EditVariantNegativeTestSource))]    
        public void EditVariantTest_WhenIndexIsOutOfRange_ShouldThrowIndexIsOutOfRangeExeption(int index, string newVariant, AbstractQuestion newQuestion)
        {
            Assert.Throws<IndexOutOfRangeException>(() => newQuestion.EditVariant(index, newVariant));
        }
        [TestCaseSource(typeof(ChangeTrueAnswerTestSource))]
        public void ChangeTrueAnswerTest(int index, string newTrueAnswer, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.ChangeTrueAnswer(index, newTrueAnswer);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }
        [TestCaseSource(typeof(ChangeTrueAnswerNegativeTestSource))]
        public void ChangeTrueAnswerTest_WhenIndexIsOutOfRange_ShouldThrowIndexIsOutOfRangeExeption(int index, string newVariant, AbstractQuestion newQuestion)
        {
            Assert.Throws<IndexOutOfRangeException>(() => newQuestion.ChangeTrueAnswer(index, newVariant));
        }
        [TestCaseSource(typeof(ChangeTrueAnswerNullTestSource))]
        public void ChangeTrueAnswerTest_WhenTrueAnswersIsNull_ShouldThrowNullReferenceExceptions(int index, string newVariant, AbstractQuestion newQuestion)
        {
            Assert.Throws<NullReferenceException>(() => newQuestion.ChangeTrueAnswer(index, newVariant));
        }
        [TestCaseSource(typeof(ChangeTrueAnswersTestSource))]
        public void ChangeTrueAnswersTest(string newVariant, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.ChangeTrueAnswer(newVariant);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }
        [TestCaseSource(typeof(AddVariantTestSource))]
        public void AddVariantTest(string newVariant, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.AddVariant(newVariant);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }
        [TestCaseSource(typeof(AddVariantNegativeTestSource))]
        public void AddVariantTest_WhenNewVariantIsNull_ShouldThrowArgumentNullException(string newVariant, AbstractQuestion newQuestion)
        {
            Assert.Throws<ArgumentNullException>(() => newQuestion.AddVariant(newVariant));
        }

        [TestCaseSource(typeof(AddTrueAnswerTestSource))]
        public void AddTrueAnswer(string newVariant, AbstractQuestion newQuestion, AbstractQuestion expectedQuestion)
        {
            AbstractQuestion actualQuestion = newQuestion;
            actualQuestion.AddTrueAnswer(newVariant);

            Assert.AreEqual(expectedQuestion, actualQuestion);
        }
        [TestCaseSource(typeof(AddTrueAnswerNegativeTestSource))]
        public void ChangeTrueAnswerTest_WhenTrueAnswersIsNull_ShouldThrowArgumentNullException(string newVariant, AbstractQuestion newQuestion)
        {
            Assert.Throws<ArgumentNullException>(() => newQuestion.AddTrueAnswer(newVariant));
        }


    }
}
