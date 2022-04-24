using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelegramBot.BL.Questions
{
    public class TypeSeveralVariants : AbstractQuestion
    {

        public TypeSeveralVariants(string discription, List<string> variants)
        {
            Discription = discription;
            Variants = variants;
        }


        public TypeSeveralVariants(string discription, List<string> trueAnswers, List<string> variants)
        {
            Discription = discription;
            Variants = variants;
            TrueAnswers = trueAnswers;
        }

    }
}

