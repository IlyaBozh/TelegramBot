using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class TypeYesOrNo : IQuestion
    {
        public string Question { get; set; }
        public bool TrueAnswer { get; set; }
        public bool UserAnswer { get; set; }

        private bool _isHasAnswered = false;


        public TypeYesOrNo(string question, bool trueAnswer, bool allAnswer)
        {
            TrueAnswer = trueAnswer;
            UserAnswer = allAnswer;
            _isHasAnswered = true;

        }
        public TypeYesOrNo(string question, bool allAnswer)
        {
            UserAnswer = allAnswer;
        }

        public void EditQuestion(string newQuestion)
        {
            Question = newQuestion;
        }

        public void ChangeTrueAnswer(bool newTrueAnswer)
        {
            TrueAnswer = newTrueAnswer;
        }

        public bool Check()
        {
            throw new Exception();
        }
    }
}

