using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeYesOrNo : AbstractQuestion
    {
        public TypeYesOrNo(string discription, string trueAnswer)
        {
            Discription = discription;
            TrueAnswer = trueAnswer;
        }

        public TypeYesOrNo(string discription)
        {
            Discription = discription;
        }

    }
}

