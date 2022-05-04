using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources
{
    internal class RemoveVariantTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("1"),
                new string("2"),
            };
            List<string> newVariants = new List<string>()
            {
                new string("1"),
            };
            List<string> variantsTwo = new List<string>()
            {
                new string("1"),
            };
            List<string> newVariantsTwo = new List<string>();



            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", newVariants);
            yield return new object[] { actualQuestion, expectedQuestion };

           
            actualQuestion = new TypeSeveralVariants("как дела?", variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", newVariantsTwo);
            yield return new object[] { actualQuestion, expectedQuestion };

        }
    }
    public class RemoveVariantNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();

            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            yield return new object[] { actualQuestion };
        }
    }
}
