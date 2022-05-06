using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BL.Askers;
using TelegramBot.BL.DataBase;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL
{
    public class TBot
    {
        private TelegramBotClient _client;
        private Action<User> _users;
        private List<long> _ids;

        private List<long> _idForTests;

        private UsersDataBase _userDataBase;

        public Dictionary<long, TestController> DataTests { get; set; }

        public TBot(string token, Action <User> users)
        {
            _client = new TelegramBotClient(token);
            _users = users;
            _ids = new List<long>();
            _userDataBase = UsersDataBase.GetInstance();
            DataTests = new Dictionary<long, TestController>();
        }

        
        public async void Send(string message, long id) /// --------------????
        {
            await _client.SendTextMessageAsync(new ChatId(id), message);
        }

        public async void Send(string message, long id, InlineKeyboardMarkup inlineKeyboardMarkup) /// --------------????
        {
            await _client.SendTextMessageAsync(new ChatId(id), message, replyMarkup: inlineKeyboardMarkup);
        }

        public async void SendFirstQuestion(List<long> ids)
        {

            _idForTests = ids;
            foreach (long id in ids)
            {
                if (DataTests[id].Questions[DataTests[id].IndexClaster].Questions.Count == DataTests[id].Counter)
                {
                    await _client.SendTextMessageAsync(new ChatId(id), "Молодец, ты прошел тестирование");
                }

                    SendInlineKeyboardButton(id);
            }
        }


        public void SendInlineKeyboardButton(long id)
        {
            int indexQuestion = DataTests[id].Counter;

            CreateButtons newButtons = new CreateButtons();

            AbstractQuestion question = DataTests[id].Questions[DataTests[id].IndexClaster].Questions[indexQuestion];

            if (question is TypeUserAnswer && question is not null)
            {
                Send(question.Description, id);
            }

            if (question is TypeOneVariant && question is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(question.Description, question.Variants);
                Send(question.Description, id, newButtonsList.ToArray());
            }

            if (question is TypeSeveralVariants && question is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(question.Description, question.Variants);

                Send(question.Description, id, newButtonsList.ToArray());
            }

            if (question is TypeYesOrNo && question is not null)
            {
                List<string> variants = new List<string> { "Да", "Нет" };
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(question.Description, variants);

                Send(question.Description, id, newButtonsList.ToArray());

            }

            if (question is TypeRightOrder && question is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(question.Description, question.Variants);

                Send(question.Description, id, newButtonsList.ToArray());
            }
            
            newButtons = null;
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

                    DataTests.Add(update.Message.Chat.Id, new TestController(update.Message.Chat.Id));
                }
                else
                {
                    if (DataTests[update.Message.Chat.Id].Questions[DataTests[update.Message.Chat.Id].IndexClaster].Questions[DataTests[update.Message.Chat.Id].Counter].setAnswer(update.Message.Text))
                    {
                        DataTests[update.Message.Chat.Id].Questions[DataTests[update.Message.Chat.Id].IndexClaster].Questions[DataTests[update.Message.Chat.Id].Counter].UserAnswer = update.Message.Text;
                        DataTests[update.Message.Chat.Id].setClasterIndex();
                        SendFirstQuestion(_idForTests);
                    }
                }

            }

            if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
            {
                bool checkMassage = DataTests[update.Message.Chat.Id].Questions[DataTests[update.Message.Chat.Id].IndexClaster].Questions[DataTests[update.Message.Chat.Id].Counter].setAnswer(update.CallbackQuery.Data);

                if (checkMassage)
                {
                    AbstractQuestion question = DataTests[update.Message.Chat.Id].Questions[DataTests[update.Message.Chat.Id].IndexClaster].Questions[DataTests[update.Message.Chat.Id].Counter];

                    if(question is TypeOneVariant || question is TypeYesOrNo)
                    {
                        question.UserAnswer = update.CallbackQuery.Data;
                    }
                    else
                    {
                        question.UserAnswers.Add(update.CallbackQuery.Data);
                    }
                }

                if (update.CallbackQuery.Data == "Готово" && !checkMassage)
                {
                    botClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, "Готово");
                    DataTests[update.Message.Chat.Id].setClasterIndex();

                    SendFirstQuestion(_idForTests);
                }    
                    
            }

        }




        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}