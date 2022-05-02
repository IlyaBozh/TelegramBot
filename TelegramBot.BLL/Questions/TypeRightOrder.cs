using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeRightOrder : AbstractQuestion
    {
        public TypeRightOrder()
        {

        }

        public TypeRightOrder(string discription, List <string> trueAnswers, List <string> variants)
        {
            Description = discription;
            TrueAnswers = trueAnswers;
            Variants = variants;
        }

        public TypeRightOrder(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;
        }
    }
}
