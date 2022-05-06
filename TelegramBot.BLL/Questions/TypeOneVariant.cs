
using TelegramBot.BL.Answerers;


namespace TelegramBot.BL.Questions
{
    public class TypeOneVariant : AbstractQuestion
    {
        public TypeOneVariant()
        {

        }

        public TypeOneVariant(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;

            answerer = new OneVariantAnswerer();
        }

        public TypeOneVariant(string discription, string trueAnswer, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswer = trueAnswer;

            answerer = new OneVariantAnswerer();
        }

        
    }
}
