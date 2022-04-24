using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL
{
    public interface IQuestion
    {
        public void EditQuestion(string newQuestion);
        public bool Check();
    }

    
}
