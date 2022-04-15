using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class QuestionToAnswer : IQuestion
    {
        public string Question { get; set; }
        public List <string> Answer { get; set; }
        public List<string> TrueAnswer { get; set; }
        public QuestionToAnswer(string question, List<string> answer, List<string> trueAnswer)
        {
            Question = question;
            Answer = answer;
            TrueAnswer = trueAnswer;
        }
        public void Create()
        {
            throw new NotImplementedException();
        }

    }
}
