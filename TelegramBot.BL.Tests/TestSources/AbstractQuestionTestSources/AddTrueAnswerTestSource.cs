using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources
{
    public class AddTrueAnswerTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> answers = new List<string>()
            {
                new string("1"),
            };
            List<string> newAnswers = new List<string>()
            {
                new string("1"),
                new string("2"),

            };
            List<string> newAnswersTwo = new List<string>()
            {
                new string("1"),
                new string("арбуз"),
            };


            string newTrueAnswer = "2";
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", newAnswers, variants);
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };

            newTrueAnswer = "арбуз";
            actualQuestion = new TypeSeveralVariants("как дела?", answers, variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", newAnswersTwo, variants);
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };
        }
    }

    public class AddTrueAnswerNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> trueAnswers = new List<string>();

            string newAnswer = null;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", trueAnswers, variants);
            yield return new object[] { newAnswer, actualQuestion };
        }
    }

}
