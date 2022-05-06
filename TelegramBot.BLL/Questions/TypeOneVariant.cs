
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
        }

        public TypeOneVariant(string discription, string trueAnswer, List<string> variants)
        {
            Description = discription;
            Variants = variants;
            TrueAnswer = trueAnswer;

        }

        public override AbstractQuestion Clone()
        {
            List<string> newVariants = new List<string>();

           foreach(string variant in Variants)
            {
                newVariants.Add(variant);
            }

            return new TypeOneVariant(this.Description, this.TrueAnswer, newVariants);
        }

        public override bool setAnswer(string massage)
        {
            if(Variants.Contains(massage))
            {
                return true;
            }

            return false;
        }
    }
}
