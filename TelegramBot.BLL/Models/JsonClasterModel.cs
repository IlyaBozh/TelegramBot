

namespace TelegramBot.BL.Models
{
    public class JsonClasterModel
    {
        public List<JsonQuestionModel> ModelQuestions { get; set; }

        public String Name { get; set; }

        public JsonClasterModel(string name, List<JsonQuestionModel> modelQuestions)
        {
            Name = name;
            ModelQuestions = modelQuestions;
        }

        public JsonClasterModel()
        {

        }
    }
}
