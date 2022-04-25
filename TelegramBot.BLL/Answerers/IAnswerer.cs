using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Answerers
{
    public interface IAnswerer
    {
        bool IsAnswerCorrect(string userAnswer, List<string> userAnswers,
                             string trueAnswer, List<string> trueAnswers);
    }
}
