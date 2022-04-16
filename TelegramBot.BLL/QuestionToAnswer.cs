using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class QuestionEnterAnswer : IQuestion
    {
        public string Question { get; set; }
        public string UserAnswers { get; set; }
        public string TrueAnswers { get; set; }
        public QuestionEnterAnswer(string question, string answer, string trueAnswer)
        {
            Question = question;
            UserAnswers = answer;
            TrueAnswers = trueAnswer;
        }
        public void Edit()
        {
            throw new NotImplementedException();
        }

    }
}
