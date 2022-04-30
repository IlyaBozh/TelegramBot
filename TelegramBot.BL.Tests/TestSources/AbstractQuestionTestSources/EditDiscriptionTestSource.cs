using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests;
using TelegramBot.BL;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources.AbstractQuestions
{
    public class EditDiscriptionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("хорошо")
            };

            List<string> TrueAnswers = new List<string>()
            {
                new string("хорошо")
            };
            string newDescription = "как дела?";
            AbstractQuestion actualQuestion = new TypeOneVariant("как настроение?", variants);
            AbstractQuestion expectedQuestion = new TypeOneVariant("как дела?", variants);
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };

            newDescription = "Расположите в правильном порядке:021293282";
            actualQuestion = new TypeRightOrder("сколько тебе лет?", variants);
            expectedQuestion = new TypeRightOrder("Расположите в правильном порядке:021293282", variants);
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };

            newDescription = "Перечислите горы";
            actualQuestion = new TypeSeveralVariants(" ", variants);
            expectedQuestion = new TypeSeveralVariants("Перечислите горы", variants);
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };

            newDescription = "你好嗎？";
            actualQuestion = new TypeUserAnswer(" ");
            expectedQuestion = new TypeUserAnswer("你好嗎？");
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };

            newDescription = "Вы голодный？";
            actualQuestion = new TypeYesOrNo(" ");
            expectedQuestion = new TypeYesOrNo("Вы голодный？");
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };
        }
    }

    public class EditDiscriptionNegativeTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("хорошо")
            };

            string newDescription = " ";
            AbstractQuestion actualQuestion = new TypeOneVariant("как настроение?", variants);
            yield return new object[] { newDescription, actualQuestion };
        }
    }
}
