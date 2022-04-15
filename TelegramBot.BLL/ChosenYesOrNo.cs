using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{ 
    public class ChosenYesOrNo : IQuestion
    {

        public string TrueAnswer { get; set; }
        public string AllAnswer { get; set; }

        public ChosenYesOrNo ( string trueAnswer, string allAnswer )
        {
            TrueAnswer = trueAnswer;
            AllAnswer = allAnswer;
        }

        public void EditQuestion()
        {
            throw new NotImplementedException();
        }
    }
}
