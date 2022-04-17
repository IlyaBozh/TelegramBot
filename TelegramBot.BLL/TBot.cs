using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System;
using System.Threading;

namespace TelegramBot.BLL
{
    public class TBot
    {
        private TelegramBotClient _client;

        public void ConnectBot(MainWindow Window)
        {
            _client = new TelegramBotClient("5149025176:AAF9ywvM1nXIkvpfKK4wV7Fsy8nTapirCDE");
            _client.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
        }

        public void Send(string text)
        {

        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var firstName = update.Message.Chat.FirstName;
            var messageText = update.Message.Text;

            _window.LBMain.Items.Add(new Label() { Content = $"{chatId} {firstName}:{messageText}" });
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}