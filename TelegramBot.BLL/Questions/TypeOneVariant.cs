using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeOneVariant : AbstractQuestion
    {
        public TypeOneVariant(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;
        }

        public TypeOneVariant(string discription, string trueAnswer, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswer = trueAnswer;
        }

        
    }
}
