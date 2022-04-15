using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{ 
    public class ChosenYesOrNo : IQuestion
    {
        public string Question { get; set; }    
        public bool TrueAnswer { get; set; }
        public bool AllAnswer { get; set; }

        public ChosenYesOrNo (string question, bool trueAnswer, bool allAnswer )
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
