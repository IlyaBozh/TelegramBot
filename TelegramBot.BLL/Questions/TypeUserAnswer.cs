
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
        }

        public TypeUserAnswer(string discription)
        {
            Description = discription;
        }

        public override AbstractQuestion Clone()
        {
            return new TypeUserAnswer(this.Description, this.TrueAnswer);
        }

        public override bool setAnswer(string massage)
        {
            return true;
        }
    }
}
