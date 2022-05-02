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
    public class ChangeTrueAnswerTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("1"),
                new string("2"),
                new string("3"),
            };
            List<string> answers = new List<string>()
            {
                new string("1"),
                new string("2"),
                new string("3"),
            };
            List<string> newAnswers = new List<string>()
            {
                new string("1"),
                new string("2"),
                new string("хорошо"),
            };
            List<string> newAnswersTwo = new List<string>()
            {
                new string("999"),
                new string("2"),
                new string("3"),
            };

            int index = 2;
            string newTrueAnswer = "хорошо";
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", newAnswers, variants);
            yield return new object[] { index, newTrueAnswer, actualQuestion, expectedQuestion };

            index = 0;
            newTrueAnswer = "999";
            actualQuestion = new TypeSeveralVariants("как дела?", answers, variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", newAnswersTwo, variants);
            yield return new object[] { index, newTrueAnswer, actualQuestion, expectedQuestion };
        }
    }
    public class ChangeTrueAnswerNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> answers = new List<string>()
            {
                new string("1"),
                new string("2"),
                new string("3"),
            };

            int index = 2;
            string newTrueAnswer = "хорошо";
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            yield return new object[] { index, newTrueAnswer, actualQuestion};

            index = -1;
            newTrueAnswer = "хорошо";
            actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            yield return new object[] { index, newTrueAnswer, actualQuestion };
        }
    }
    public class ChangeTrueAnswerNullTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();

            int index = 0;
            string newTrueAnswer = "хорошо";
            AbstractQuestion actualQuestion = new TypeOneVariant("как дела?", "плохо", variants);
            yield return new object[] { index, newTrueAnswer, actualQuestion };

            index = 0;
            newTrueAnswer = "хорошо";
            actualQuestion = new TypeUserAnswer("как дела?", "плохо");
            yield return new object[] { index, newTrueAnswer, actualQuestion };

            index = 0;
            newTrueAnswer = "хорошо";
            actualQuestion = new TypeYesOrNo("как дела?", "плохо");
            yield return new object[] { index, newTrueAnswer, actualQuestion };
        }
    }
}