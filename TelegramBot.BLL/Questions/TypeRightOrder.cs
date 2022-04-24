using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeRightOrder : AbstractQuestion
    {

        public  string Question { get; set; }

        public List <string> AllAnswers { get; set; }
        public List <string> TrueAnswers { get; set; }
        public List <string> UserAnswers { get; set; }


        public TypeRightOrder(string question, List <string> trueAnswers, List <string> allAnswers)
        {
            Question = question;
            AllAnswers = allAnswers;
            TrueAnswers = trueAnswers;

        }

        public void EditQuestion(string newQuestion)
        {
            throw new NotImplementedException();
        }

        public bool Check()
        {
            throw new NotImplementedException();
        }
    }
}
