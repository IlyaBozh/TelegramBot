using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Answerers;
using TelegramBot.BL.Askers;

namespace TelegramBot.BL.Questions
{
    public class TypeSeveralVariants : AbstractQuestion
    {

        public TypeSeveralVariants()
        {

        }

        public TypeSeveralVariants(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;

            asker = new SeveralVariantsAsker();
            answerer = new SeveralVariantsAnswerer();
        }


        public TypeSeveralVariants(string discription, List<string> trueAnswers, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswers = trueAnswers;

            asker = new SeveralVariantsAsker();
            answerer = new SeveralVariantsAnswerer();
        }

    }
}

