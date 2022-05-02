using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL.Questions
{
    public enum QuestionType
    { 
        UserAnswer,
        OneVariant,
        SeveralVariants,
        YesOrNo,
        RightOrder
    }




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
