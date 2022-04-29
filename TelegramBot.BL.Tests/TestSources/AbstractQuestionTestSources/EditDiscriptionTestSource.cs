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
                new string("njg")
            };
            
            string newDescription = "вопросы математика";
            AbstractQuestion actualQuestion = new TypeOneVariant("уравнение", variants);
            AbstractQuestion expectedQuestion = new TypeOneVariant("вопросы математика", variants);
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };



        }
    }
}
