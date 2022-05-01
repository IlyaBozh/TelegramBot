using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests;
using TelegramBot.BL.Questions;
using System.Collections;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources
{
    public class AddVariantTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();
            List<string> answers = new List<string>();
            List<string> newVariant = new List<string>()
            {
                new string("1"),
            };
            List<string> newVariantTwo= new List<string>()
            {
                new string("арбуз"),
            };


            string newTrueAnswer = "1";
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", answers, variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", answers, newVariant);
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };

            newTrueAnswer = "арбуз";
            actualQuestion = new TypeSeveralVariants("как дела?", answers, variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", answers, newVariantTwo);
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };

        }
    }
    public class AddVariantNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();

            string newTrueAnswer = null;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            yield return new object[] { newTrueAnswer, actualQuestion};
        }
    }

}
