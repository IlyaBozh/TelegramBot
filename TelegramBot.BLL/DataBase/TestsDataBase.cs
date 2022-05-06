using System.Text.Json;
using TelegramBot.BL.Questions;
using TelegramBot.BL.Models;

namespace TelegramBot.BL.DataBase
{
    public class TestsDataBase
    {
        public List<ClasterQuestions> Tests { get; set; }
        public List<ClasterQuestions> Polls { get; set; }
        public List<AbstractQuestion> TestSingelQuestions { get; set; }
        public List<AbstractQuestion> PollSingelQuestions { get; set; }

        private static TestsDataBase _instance;

        private const string filePathTestSingelQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\TestSingelQuestions.asd";
        private const string filePathPollSingelQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\PollSingelQuestions.asd";
        private const string filePathTestQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\TestQuestions.asd";
        private const string filePathPollQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\PollQuestions.asd";

        private TestsDataBase()
        {
            Tests = new List<ClasterQuestions>();
            Polls = new List<ClasterQuestions>();
            TestSingelQuestions = new List<AbstractQuestion>();
            PollSingelQuestions = new List<AbstractQuestion>();
        }

        public static TestsDataBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestsDataBase();
            }
            return _instance;
        }


        public string SerializeClasterModel(List<JsonClasterModel> question)
        {
            return JsonSerializer.Serialize<List<JsonClasterModel>>(question);
        }


        public string SerializeQuestionModel(List<JsonQuestionModel> question)
        {
            return JsonSerializer.Serialize<List<JsonQuestionModel>>(question);
        }

        public string SerializeQuestion(AbstractQuestion question)
        {
            return JsonSerializer.Serialize<AbstractQuestion>(question);
        }


        public List<JsonClasterModel> DecerializeClasterModel(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                return JsonSerializer.Deserialize<List<JsonClasterModel>>(json);
            }
        }


        public List<JsonQuestionModel> DecerializeQuestionModel(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                return JsonSerializer.Deserialize<List<JsonQuestionModel>>(json);
            }
        }


        public AbstractQuestion DecerializeQuestion(JsonQuestionModel questionModel)
        {
            string json = questionModel.Json;
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                AbstractQuestion question;
                switch (questionModel.Type)
                {
                    case QuestionType.OneVariant:
                        question = JsonSerializer.Deserialize<TypeOneVariant>(json);
                        break;

                    case QuestionType.SeveralVariants:
                        question = JsonSerializer.Deserialize<TypeSeveralVariants>(json);
                        break;

                    case QuestionType.RightOrder:
                        question = JsonSerializer.Deserialize<TypeRightOrder>(json);
                        break;

                    case QuestionType.UserAnswer:
                        question = JsonSerializer.Deserialize<TypeUserAnswer>(json);
                        break;

                    default:
                        question = JsonSerializer.Deserialize<TypeYesOrNo>(json);
                        break;
                }

                return question;
            }
        }



        public List<JsonQuestionModel> FillModel(List<AbstractQuestion> singelQuestions)
        {
            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();
            QuestionType type;
            string json;

            if (singelQuestions != null)
            {
                foreach (var question in singelQuestions)
                {
                    switch (question)
                    {
                        case TypeOneVariant:
                            type = QuestionType.OneVariant;
                            json = SerializeQuestion(question);
                            break;

                        case TypeRightOrder:
                            type = QuestionType.RightOrder;
                            json = SerializeQuestion(question);
                            break;

                        case TypeSeveralVariants:
                            type = QuestionType.SeveralVariants;
                            json = SerializeQuestion(question);
                            break;

                        case TypeUserAnswer:
                            type = QuestionType.UserAnswer;
                            json = SerializeQuestion(question);
                            break;

                        default:
                            type = QuestionType.YesOrNo;
                            json = SerializeQuestion(question);
                            break;
                    }
                    questionModels.Add(new JsonQuestionModel(type, json));
                }
            }

            return questionModels;
        }



        public void SaveSingelTest(List<AbstractQuestion> TestSingelQuestions)
        {
            List<JsonQuestionModel> questionModels = FillModel(TestSingelQuestions);

            string json = SerializeQuestionModel(questionModels);

            using (StreamWriter sw = new StreamWriter(filePathTestSingelQuestions, false))
            {
                sw.WriteLine(json);
            }

        }

        public void SaveSingelPoll(List<AbstractQuestion> PollSingelQuestions)
        {
            List<JsonQuestionModel> questionModels = FillModel(PollSingelQuestions);

            string json = SerializeQuestionModel(questionModels);

            using (StreamWriter sw = new StreamWriter(filePathPollSingelQuestions, false))
            {
                sw.WriteLine(json);
            }
        }


        public void SaveClasterTest(List<ClasterQuestions> Tests)
        {
            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();

            foreach (ClasterQuestions test in Tests)
            {
                questionModels = FillModel(test.Questions);
                clasterModels.Add(new JsonClasterModel(test.NameClaster, questionModels));
            }

            string json = SerializeClasterModel(clasterModels);

            using (StreamWriter sw = new StreamWriter(filePathTestQuestions, false))
            {
                sw.WriteLine(json);
            }

        }

        public void SaveClasterPoll(List<ClasterQuestions> Polls)
        {
            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();

            foreach (ClasterQuestions test in Polls)
            {
                questionModels = FillModel(test.Questions);
                clasterModels.Add(new JsonClasterModel(test.NameClaster, questionModels));
            }

            string json = SerializeClasterModel(clasterModels);

            using (StreamWriter sw = new StreamWriter(filePathPollQuestions, false))
            {
                sw.WriteLine(json);
            }
        }


        public List<AbstractQuestion> LoadSingelTest()
        {
            List<List<AbstractQuestion>> singelQuestionClasters = new List<List<AbstractQuestion>>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();

            using (StreamReader sr = new StreamReader(filePathTestSingelQuestions))
            {
                string json = sr.ReadLine();
                questionModels = DecerializeQuestionModel(json);
            }

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            foreach (JsonQuestionModel question in questionModels)
            {

                questions.Add(DecerializeQuestion(question));
            }

            return questions;
        }

        public List<AbstractQuestion> LoadSingelPoll()
        {
            List<List<AbstractQuestion>> singelQuestionClasters = new List<List<AbstractQuestion>>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();


            using (StreamReader sr = new StreamReader(filePathPollSingelQuestions))
            {
                string json = sr.ReadLine();
                questionModels = DecerializeQuestionModel(json);
            }

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            foreach (JsonQuestionModel question in questionModels)
            {
                questions.Add(DecerializeQuestion(question)) ;
            }

            return questions;
        }


        public List<ClasterQuestions> LoadClasterTests()
        {

            List<ClasterQuestions> clasterOfQuestions = new List<ClasterQuestions>();

            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            using (StreamReader sr = new StreamReader(filePathTestQuestions))
            {
                string json = sr.ReadLine();
                clasterModels = DecerializeClasterModel(json);
            }

            foreach (JsonClasterModel claster in clasterModels)
            {
                foreach (JsonQuestionModel question in claster.ModelQuestions)
                {
                    questions.Add(DecerializeQuestion(question));
                }

                clasterOfQuestions.Add(new ClasterQuestions(claster.Name, questions));
                questions = new List<AbstractQuestion>();
            }

            return clasterOfQuestions;
        }

        public List<ClasterQuestions> LoadClasterPolls()
        {
            List<ClasterQuestions> clasterOfQuestions = new List<ClasterQuestions>();

            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            using (StreamReader sr = new StreamReader(filePathPollQuestions))
            {
                string json = sr.ReadLine();
                clasterModels = DecerializeClasterModel(json);
            }

            foreach (JsonClasterModel claster in clasterModels)
            {
                foreach (JsonQuestionModel question in claster.ModelQuestions)
                {
                    questions.Add(DecerializeQuestion(question));
                }

                clasterOfQuestions.Add(new ClasterQuestions(claster.Name, questions));
                questions = new List<AbstractQuestion>();
            }

            return clasterOfQuestions;
        }

    }
}
