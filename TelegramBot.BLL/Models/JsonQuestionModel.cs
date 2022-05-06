

namespace TelegramBot.BL.Models
{

    public class JsonQuestionModel
    {
        public QuestionType Type { get; set; }

        public string Json { get; set; }

        public JsonQuestionModel(QuestionType type, string json)
        {
            Type = type;
            Json = json;
        }

        public JsonQuestionModel()
        {

        }

    }
}
