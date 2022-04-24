using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public class TypeUserAnswer : AbstractQuestion
    {
        public string Question { get; set; }
        public string UserAnswer { get; set; }
        public string TrueAnswer { get; set; }
        private bool _isHasAnswered = false;
        public TypeUserAnswer(string question, string answer, string trueAnswer)
        {
            Question = question;
            UserAnswer = answer;
            TrueAnswer = trueAnswer;
        }

        public TypeUserAnswer(string question, string answer)
        {
            Question = question;
            UserAnswer = answer;
        }
        public void EditQuestion(string newQuestion)
        {
            Question = newQuestion;
        }

        public void ChangeTrueAnswer(string newTrueAnswer)
        {
            TrueAnswer = newTrueAnswer;
        }
        public void RemoveTrueAnswers()
        {
            TrueAnswer = "";
        }

        public bool Check()
        {
            throw new Exception();
        }

    }
}
