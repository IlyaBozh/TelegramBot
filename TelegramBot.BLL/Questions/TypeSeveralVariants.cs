
using TelegramBot.BL.Answerers;


namespace TelegramBot.BL.Questions
{
    public class TypeSeveralVariants : AbstractQuestion
    {

        public TypeSeveralVariants()
        {

        }

        public TypeSeveralVariants(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;

            answerer = new SeveralVariantsAnswerer();
        }


        public TypeSeveralVariants(string discription, List<string> trueAnswers, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswers = trueAnswers;

            answerer = new SeveralVariantsAnswerer();
        }

    }
}

