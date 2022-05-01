using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestionTestSources
{
    internal class RemoveVariantByIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("1"),
                new string("2"),
                new string("3"),
            };
            List<string> newVariants = new List<string>()
            {
                new string("2"),
                new string("3"),
            };
            List<string> newVariantsTwo = new List<string>()
            {
                new string("1"),
                new string("3"),
            };


            int index = 0;  
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            AbstractQuestion expectedQuestion = new TypeRightOrder("как дела?", newVariants);
            yield return new object[] { index, actualQuestion, expectedQuestion };

            index = 1;
            actualQuestion = new TypeSeveralVariants("как дела?", variants);
            expectedQuestion = new TypeSeveralVariants("как дела?", newVariantsTwo);
            yield return new object[] { index, actualQuestion, expectedQuestion };
        }
    }
    public class RemoveVariantByIndexNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("1"),
                new string("2"),
            };

            int index = -1;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            yield return new object[] {index, actualQuestion };

            index = 3;
            actualQuestion = new TypeRightOrder("как дела?", variants);
            yield return new object[] { index, actualQuestion };
        }
    }
    public class RemoveVariantByIndexEmptyTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>();

            int index = 0;
            AbstractQuestion actualQuestion = new TypeRightOrder("как дела?", variants);
            yield return new object[] { index, actualQuestion };
        }
    }
}
