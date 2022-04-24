using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BLL
{
    public class Poll
    {
        List<AbstractQuestion> Questions { get; set; }
    }
}
