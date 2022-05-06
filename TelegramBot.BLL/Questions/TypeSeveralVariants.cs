
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
        }


        public TypeSeveralVariants(string discription, List<string> trueAnswers, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswers = trueAnswers;
        }

        public override AbstractQuestion Clone()
        {
            List<string> newVariants = new List<string>();

            foreach (var variant in Variants)
            {
                newVariants.Add(variant);
            }

            List<string> newTrueAnswers = new List<string>();

            foreach (var answer in TrueAnswers)
            {
                newTrueAnswers.Add(answer);
            }

            return new TypeSeveralVariants(this.Description, newTrueAnswers, newVariants);
        }

        public override bool setAnswer(string message)
        {
            if(message == "Готово")
            { 
                return true;
            }

            return false;
        }
    }
}

