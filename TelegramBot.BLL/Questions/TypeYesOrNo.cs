
using TelegramBot.BL.Answerers;


namespace TelegramBot.BL.Questions
{
    public class TypeYesOrNo : AbstractQuestion
    {
        public TypeYesOrNo()
        {

        }

        public TypeYesOrNo(string discription, string trueAnswer)
        {
            Description = discription;
            TrueAnswer = trueAnswer;

            answerer = new YesOrNoAnswerer();
        }

        public TypeYesOrNo(string discription)
        {
            Description = discription;

            answerer = new YesOrNoAnswerer();
        }

    }
}

