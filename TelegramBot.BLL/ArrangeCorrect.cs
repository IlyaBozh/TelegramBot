using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    internal class ArrangeCorrect : IQuestion
    {

        public  string Question { get; set; }

        public List <string> AllAnswers { get; set; }
        public List <string> TrueAnswer { get; set; }



        public ArrangeCorrect(string question, List <string> trueAnswer, List <string> allAnswer)
        {
            Question = question;
            AllAnswers = allAnswer;
            TrueAnswer = trueAnswer;

        }


        public void EditQuestion()
        {
            throw new NotImplementedException();
        }
    }
}
