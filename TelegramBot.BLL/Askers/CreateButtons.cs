using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.BL.Askers
{
    public class CreateButtons
    {
        public List <InlineKeyboardButton[]> AddButtons(string description, List<string> variants)
        {
            var buttons = variants.Select(answers => new[] { new KeyboardButton(answers) }).ToArray();          
            List<InlineKeyboardButton[]> inlineKeyboardButton = new List<InlineKeyboardButton[]>();
            var done = new[] { InlineKeyboardButton.WithCallbackData(text: "Готово", callbackData: "Готово") };

            for (var i = 0; i < buttons.Length; i++)
            {
                var keyboardButtons = new[] { InlineKeyboardButton.WithCallbackData(text: $"{variants[i]}", callbackData: $"{variants[i]}") };

                inlineKeyboardButton.Add(keyboardButtons);
            }


            inlineKeyboardButton.Add(done);
            inlineKeyboardButton.ToArray();

            return inlineKeyboardButton;
        }
    }
}
