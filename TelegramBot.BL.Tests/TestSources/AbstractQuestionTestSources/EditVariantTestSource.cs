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
    public class EditVariantTestSource: IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("замечательно"),
                new string("хорошо"),
                new string("отлично"),
                new string("невероятно"),
            };
            List<string> newVariants = new List<string>()
            {
                new string("замечательно"),
                new string("хорошо"),
                new string("плохо"),
                new string("невероятно"),
            };
            List<string> newVariantsOne = new List<string>()
            {
                new string("замечательно"),
                new string("хорошо"),
                new string("отлично"),
                new string("плохо"),
            };
            List<string> newVariantsTwo = new List<string>()
            {
                new string("плохо"),
                new string("хорошо"),
                new string("отлично"),
                new string("невероятно"),
            };

            int index = 2;
            string newVariant = "плохо";
            AbstractQuestion actualQuestion = new TypeOneVariant("как настроение?", variants);
            AbstractQuestion expectedQuestion = new TypeOneVariant("как настроение?", newVariants);
            yield return new object[] { index, newVariant, actualQuestion, expectedQuestion };


            index = 3;
            newVariant = "плохо";
            actualQuestion = new TypeRightOrder("как настроение?", variants);
            expectedQuestion = new TypeRightOrder("как настроение?", newVariantsOne);
            yield return new object[] { index, newVariant, actualQuestion, expectedQuestion };

            index = 0;
            newVariant = "плохо";
            actualQuestion = new TypeSeveralVariants("как настроение?", variants);
            expectedQuestion = new TypeSeveralVariants("как настроение?", newVariantsTwo);
            yield return new object[] { index, newVariant, actualQuestion, expectedQuestion };
        }
    }
    public class EditVariantNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("замечательно"),
                new string("хорошо"),
            };

            int index = 3;
            string newVariant = "плохо";
            AbstractQuestion actualQuestion = new TypeOneVariant("как настроение?", variants);
            yield return new object[] { index, newVariant, actualQuestion};

            index = -1;
            newVariant = "плохо";
            actualQuestion = new TypeOneVariant("как настроение?", variants);
            yield return new object[] { index, newVariant, actualQuestion };

        }
    }

}
