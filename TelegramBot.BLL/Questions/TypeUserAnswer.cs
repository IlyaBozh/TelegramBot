using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public TypeUserAnswer(string discription)
        {
            Description = discription;
        }

    }
}
