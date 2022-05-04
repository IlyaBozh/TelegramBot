﻿using System;
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
            var keyboardButtons = new InlineKeyboardButton[buttons.Length];
            List<InlineKeyboardButton[]> inlineKeyboardButton = new List<InlineKeyboardButton[]>();

            var done = new[] { InlineKeyboardButton.WithCallbackData(text: "Готово", callbackData: "Готово") };



            for (var i = 0; i < buttons.Length; i++)
            {
                var btn = new InlineKeyboardButton[i];
                {
                    string oneAnswer = variants[i],
                    CallbackData = variants[i];
                }

                keyboardButtons[i] = variants[i];
            }


            inlineKeyboardButton.Add(keyboardButtons);
            inlineKeyboardButton.Add(done);
            inlineKeyboardButton.ToArray();

            return inlineKeyboardButton;
        }
    }
}
