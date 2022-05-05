using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BL.DataBase;

namespace TelegramBot.BL
{
    public class TBot
    {
        private TelegramBotClient _client;
        private Action<User> _users;
        private List<long> _ids;
        private Dictionary<long, TestCounter> _group;

        private UsersDataBase _userDataBase;

        Dictionary<long, string> _usersDict;

        public TBot(string token, Action <User> users)
        {
            _client = new TelegramBotClient(token);
            _users = users;
            _ids = new List<long>();
            _userDataBase = UsersDataBase.GetInstance();
            _usersDict = new Dictionary<long, string>();
        }

        
        public async void Send(string message, long id) 
        {

            await _client.SendTextMessageAsync(new ChatId(id), message);

        }

        public async void Send(string message, long id, InlineKeyboardMarkup inlineKeyboardMarkup) 
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
                if (_group != null)
                {
                    
                }
            }
            
            else if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
            {

                //List<string> UserAnswers = new List<string>();
                //foreach (string answer in (string)update.CallbackQuery.Data)
                //{
                //    UserAnswers.Add(answer);
                //}
                //xhto.Add($"Ответ пользователя:{update.CallbackQuery.Data}".ToString()); // это записать в лист useranswer

                if (update.CallbackQuery.Data == "Готово")
                {
                    botClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, "Спасибо за ответ!");
                }    
                    
            }

        }

      





        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}