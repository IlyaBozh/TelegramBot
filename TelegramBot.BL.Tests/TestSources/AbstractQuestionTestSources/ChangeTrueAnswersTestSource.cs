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
    public class ChangeTrueAnswersTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> variants = new List<string>()
            {
                new string("хорошо"),
                new string("плохо"),
            };
            
            string newTrueAnswer = "хорошо";
            AbstractQuestion actualQuestion = new TypeOneVariant("как дела?", "gkj[j", variants);
            AbstractQuestion expectedQuestion = new TypeOneVariant("как дела?", "хорошо", variants);
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };

            newTrueAnswer = "хорошо";
            actualQuestion = new TypeUserAnswer("как дела?", "gkj[j");
            expectedQuestion = new TypeUserAnswer("как дела?", "хорошо");
            yield return new object[] { newTrueAnswer, actualQuestion, expectedQuestion };
        }
    }
}
