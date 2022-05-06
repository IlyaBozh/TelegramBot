
using TelegramBot.BL.Answerers;


namespace TelegramBot.BL.Questions
{
    public class TypeRightOrder : AbstractQuestion
    {
        public TypeRightOrder()
        {

        }

        public TypeRightOrder(string discription, List <string> trueAnswers, List <string> variants)
        {
            Description = discription;
            TrueAnswers = trueAnswers;
            Variants = variants;

            answerer = new RightOrderAnswerer();
        }

        public TypeRightOrder(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;

            answerer = new RightOrderAnswerer();
        }
    }
}
