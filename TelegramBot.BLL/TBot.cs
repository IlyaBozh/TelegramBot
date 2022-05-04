using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegramBot.BL
{
    public class TBot
    {
        private TelegramBotClient _client;
        private Action<User> _users;
        private List<long> _ids;
        

        public TBot(string token, Action <User> users)
        {
            _client = new TelegramBotClient(token);
            _users = users;
            _ids = new List<long>();
        }

        
        public async void Send(string message, long id) /// --------------????
        {

            await _client.SendTextMessageAsync(new ChatId(id), message);

        }

        public async void Send(string message, long id, InlineKeyboardMarkup inlineKeyboardMarkup) /// --------------????
        {


            await _client.SendTextMessageAsync(new ChatId(id), message, replyMarkup: inlineKeyboardMarkup);


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
                    User newUser = new User(update.Message.Chat.FirstName!, update.Message.Chat.LastName!, update.Message.Chat.Id);
                    _users(newUser);
                    
                }
               

               
            }

            else if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
            {
                if(update.CallbackQuery.Data == "Готово")
                {
                      botClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, "Готово");


                }    
                    
            }

        }

      





        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}