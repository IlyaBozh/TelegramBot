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
        public List <string> Answers { get; set; }
        public List<string> TrueAnswers { get; set; }
        public QuestionEnterAnswer(string question, List<string> answer, List<string> trueAnswer)
        {
            Question = question;
            Answers = answer;
            TrueAnswers = trueAnswer;
        }
        public void Edit()
        {
            throw new NotImplementedException();
        }

    }
}
