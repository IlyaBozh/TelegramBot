using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.BLL
{
    public class TBot
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        private List<long> _ids;

        public TBot(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _ids = new List<long>();
        }

        public async void Send(string s)
        {
            foreach (var id in _ids)
            {
                await _client.SendTextMessageAsync(new ChatId(id), s);
            }
        }

        public void Start()
        {
            _client.StartReceiving(HandleResive, HandleError);
        }

        private async Task HandleResive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null && update.Message.Text != null)
            {

                if (!_ids.Contains(update.Message.Chat.Id))
                {
                    _ids.Add(update.Message.Chat.Id);
                }

                string s = update.Message.Chat.FirstName + " "
                    + update.Message.Chat.LastName + " ";
                    //+ update.Message.Text;
                _onMessage(s);
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}