using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Tests;
using TelegramBot.BL;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.Tests.TestSources
{
    public class EditDiscriptionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> list = new List<string>();

            string newDescription = "вопросы математика";
            AbstractQuestion actualQuestion = new TypeOneVariant("уравнение", list);
            AbstractQuestion expectedQuestion = new TypeOneVariant("вопросы математика", list);
            yield return new object[] { newDescription, actualQuestion, expectedQuestion };



        }
    }
}
