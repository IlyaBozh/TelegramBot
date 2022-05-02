using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
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
