using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources
{
    internal class RemoveTrueAnswerByIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> answers = new List<string>()
            {
                new string("1"),
                new string("2"),
            };
            List<string> newAnswers = new List<string>()
            {
                new string("2"),
            };
            List<string> answersTwo = new List<string>()
            {
                new string("1"),
                new string("2"),
            };
            List<string> newAnswersTwo = new List<string>()
            {
                new string("1"),
            };

            int index = 0;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", newAnswers, variants);
            yield return new object[] { index, actualQuestion, expectedQuestion };

            index = 1;
            actualQuestion = new TypeSeveralVariants("как дела?", answersTwo, variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", newAnswersTwo, variants);
            yield return new object[] { index, actualQuestion, expectedQuestion };
        }
    }
    public class RemoveTrueAnswerByIndexNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> answers = new List<string>()
            {
                new String("2"),
                new String("1"),
            };

            int index = -1;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            yield return new object[] { index, actualQuestion };

            index = 3;
            actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            yield return new object[] { index, actualQuestion };
        }
    }

}
