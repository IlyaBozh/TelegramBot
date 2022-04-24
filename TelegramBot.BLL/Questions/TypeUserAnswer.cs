using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeUserAnswer : AbstractQuestion
    {
        public TypeUserAnswer(string discription, string trueAnswer)
        {
            Discription = discription;
            TrueAnswer = trueAnswer;
        }

        public TypeUserAnswer(string discription)
        {
            Discription = discription;
        }

    }
}
