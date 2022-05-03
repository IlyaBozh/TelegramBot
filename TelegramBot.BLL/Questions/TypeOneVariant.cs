using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Answerers;
using TelegramBot.BL.Askers;

namespace TelegramBot.BL.Questions
{
    public class TypeOneVariant : AbstractQuestion
    {
        public TypeOneVariant()
        {

        }

        public TypeOneVariant(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;

            //asker = new OneVariantAsker();
            answerer = new OneVariantAnswerer();
        }

        public TypeOneVariant(string discription, string trueAnswer, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswer = trueAnswer;

           // asker = new OneVariantAsker();
            answerer = new OneVariantAnswerer();
        }

        
    }
}
