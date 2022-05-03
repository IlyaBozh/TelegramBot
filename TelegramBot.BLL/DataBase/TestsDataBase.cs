using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.DataBase
{
    public class TestsDataBase
    {
        public List<Claster> Tests { get; set; }
        public List<Claster> Polls { get; set; }
        public List<AbstractQuestion> TestSingelQuestions { get; set; }
        public List<AbstractQuestion> PollSingelQuestions { get; set; }

        private static TestsDataBase _instance;

        private const string filePathTestSingelQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\TestSingelQuestions.asd";
        private const string filePathPollSingelQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\PollSingelQuestions.asd";
        private const string filePathTestQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\TestQuestions.asd";
        private const string filePathPollQuestions = @"C:\Users\Иля\source\repos\TelegramBot\QuestionFiles\PollQuestions.asd";

        private TestsDataBase()
        {
            Tests = new List<Claster>();
            Polls = new List<Claster>();
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



        public void SaveSingel(List<AbstractQuestion> TestSingelQuestions, List<AbstractQuestion> PollSingelQuestions)
        {
            List<JsonQuestionModel> questionModels = FillModel(TestSingelQuestions);

            string json = SerializeQuestionModel(questionModels);

            using (StreamWriter sw = new StreamWriter(filePathTestSingelQuestions, false))
            {
                sw.WriteLine(json);
            }

            questionModels = FillModel(PollSingelQuestions);

            json = SerializeQuestionModel(questionModels);

            using (StreamWriter sw = new StreamWriter(filePathPollSingelQuestions, false))
            {
                sw.WriteLine(json);
            }
        }


        public void SaveClaster(List<Claster> Tests, List<Claster> Polls)
        {
            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();

            foreach (Claster test in Tests)
            {
                questionModels = FillModel(test.Questions);
                clasterModels.Add(new JsonClasterModel(test.NameClaster, questionModels));
            }

            string json = SerializeClasterModel(clasterModels);

            using (StreamWriter sw = new StreamWriter(filePathTestQuestions, false))
            {
                sw.WriteLine(json);
            }

            clasterModels.Clear();
            questionModels.Clear();

            foreach (Claster test in Polls)
            {
                questionModels = FillModel(test.Questions);
                clasterModels.Add(new JsonClasterModel(test.NameClaster, questionModels));
            }

            json = SerializeClasterModel(clasterModels);

            using (StreamWriter sw = new StreamWriter(filePathPollQuestions, false))
            {
                sw.WriteLine(json);
            }
        }


        public List<List<AbstractQuestion>> LoadSingel()
        {
            List<List<AbstractQuestion>> singelQuestionClasters = new List<List<AbstractQuestion>>();

            List<JsonQuestionModel> questionModels = new List<JsonQuestionModel>();

            using (StreamReader sr = new StreamReader(filePathTestSingelQuestions))
            {
                string json = sr.ReadLine();
                questionModels = DecerializeQuestionModel(json);
            }

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            foreach(JsonQuestionModel question in questionModels)
            {

                questions.Add(DecerializeQuestion(question));
            }

            singelQuestionClasters.Add(questions);

            questions = new List<AbstractQuestion>();

            using (StreamReader sr = new StreamReader(filePathPollSingelQuestions))
            {
                string json = sr.ReadLine();
                questionModels = DecerializeQuestionModel(json);
            }

            foreach (JsonQuestionModel question in questionModels)
            {
                questions.Add(DecerializeQuestion(question));
            }

            singelQuestionClasters.Add(questions);

            return singelQuestionClasters;
        }

        public List<List<Claster>> LoadClaster()
        {
            List<List<Claster>> ListOfClasters = new List<List<Claster>>();

            List<Claster> clasterOfQuestions = new List<Claster>();

            List<JsonClasterModel> clasterModels = new List<JsonClasterModel>();

            List<AbstractQuestion> questions = new List<AbstractQuestion>();

            using (StreamReader sr = new StreamReader(filePathTestQuestions))
            {
                string json = sr.ReadLine();
                clasterModels = DecerializeClasterModel(json);
            }

            foreach(JsonClasterModel claster in clasterModels)
            {
                foreach(JsonQuestionModel question in claster.ModelQuestions)
                {
                    questions.Add(DecerializeQuestion(question));
                }

                clasterOfQuestions.Add(new Claster(claster.Name, questions));
            }

            ListOfClasters.Add(clasterOfQuestions);

            clasterOfQuestions = new List<Claster>();
            questions.Clear();


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

                clasterOfQuestions.Add(new Claster(claster.Name, questions));
            }

            ListOfClasters.Add(clasterOfQuestions);
            return ListOfClasters;
        }

    }
}
