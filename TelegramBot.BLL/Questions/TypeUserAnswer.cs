using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Answerers;
using TelegramBot.BL.Askers;

namespace TelegramBot.BL.Questions
{
    public class TypeUserAnswer : AbstractQuestion
    {
        public TypeUserAnswer()
        {

        }

        public TypeUserAnswer(string discription, string trueAnswer)
        {
            Description = discription;
            TrueAnswer = trueAnswer;

            answerer = new UserAnswerAnswerer();
        }

        public TypeUserAnswer(string discription)
        {
            Description = discription;

            answerer = new UserAnswerAnswerer();
        }

    }
}
