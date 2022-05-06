
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
        }

        public TypeRightOrder(string discription, List<string> variants)
        {
            Description = discription;
            Variants = variants;
        }

        public override AbstractQuestion Clone()
        {
            List<string> newVariants = new List<string>();

            foreach(var variant in Variants)
            {
                newVariants.Add(variant);
            }

            List<string> newRightOrder = new List<string>();

            foreach (var answer in TrueAnswers)
            {
                newRightOrder.Add(answer);
            }

            return new TypeRightOrder(this.Description, newRightOrder, newVariants);
        }

        public override bool setAnswer(string massage)
        {
            if(Variants.Count == TrueAnswers.Count)
            {
                return true;
            }

            return false;
        }
    }
}
