


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

        }

        public TypeYesOrNo(string discription)
        {
            Description = discription;
        }

        public override AbstractQuestion Clone()
        {
            return new TypeYesOrNo(this.Description, this.TrueAnswer);
        }

        public override bool setAnswer(string massage)
        {
            if(massage == "Да" || massage == "Нет")
            {
                return true;
            }

            return true;
        }
    }
}

