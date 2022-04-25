using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Askers
{
    public interface IAsker
    {
        void Ask(string description, List<string> variants);
    }
}
