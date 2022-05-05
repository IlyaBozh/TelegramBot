using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Answerers;
using TelegramBot.BL.Askers;

namespace TelegramBot.BL.Questions
{
    public class TypeYesOrNo : AbstractQuestion
    {
        public TypeYesOrNo()
        {

        }

        public TypeYesOrNo(string discription, string trueAnswer)
        {
            Description = discription;
            TrueAnswer = trueAnswer;

            answerer = new YesOrNoAnswerer();
        }

        public TypeYesOrNo(string discription)
        {
            Description = discription;

            answerer = new YesOrNoAnswerer();
        }

    }
}

