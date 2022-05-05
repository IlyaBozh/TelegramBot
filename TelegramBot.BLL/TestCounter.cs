using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL
{
    public class TestCounter
    {
        public List<AbstractQuestion> Questions { get; set; }
        public int Counter { get; set; }

        public TestCounter(List<AbstractQuestion> questions)
        {
            Questions = questions;
            Counter = 0;
        }
        public void IncreaseCounter()
        {
            Counter++;
        }
    }
}
