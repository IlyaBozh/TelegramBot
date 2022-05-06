
using TelegramBot.BL.Answerers;


namespace TelegramBot.BL.Questions
{
    public class TypeUserAnswer : AbstractQuestion
    {
        public TypeUserAnswer()
        {

        }

        public TypeUserAnswer(string discription, string trueAnswer)
        {
            Description = discription;
            TrueAnswer = trueAnswer;

            answerer = new UserAnswerAnswerer();
        }

        public TypeUserAnswer(string discription)
        {
            Description = discription;

            answerer = new UserAnswerAnswerer();
        }

    }
}
