using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Answerers;
using TelegramBot.BL.Askers;

namespace TelegramBot.BL.Questions
{
    public abstract class AbstractQuestion
    {
        public string Description { get; set; }
        public List<string> Variants { get; set; }
        public string TrueAnswer { get; set; }  
        public List<string> TrueAnswers { get; set; }
        /*public bool AnswerYesOrNo { get; set; }*/
        public string UserAnswer { get; set; }
        public List<string> UserAnswers { get; set; }
        /*public bool UserAnswerYesOrNo { get; set; }*/

       

        protected IAsker asker;
        protected IAnswerer answerer;

        public void Ask()
        {
            asker.Ask(Description, Variants);
        }

        public bool IsAnswerCorrect()
        {
            return answerer.IsAnswerCorrect(UserAnswer, UserAnswers, TrueAnswer, TrueAnswers);
        }

        public void EditDiscription(string newDescription)
        {
            Description = newDescription;
        }

        public void EditVariant(int index, string newVariant, bool isVariant = true)
        {
            if (isVariant)
            {
                Variants[index] = newVariant;
            }
            else
            {
                TrueAnswers[index] = newVariant;
            }
        }

        public void ChangeTrueAnswer (string newTrueAnswer)
        {
            TrueAnswer = newTrueAnswer;
        }

        public void AddVariant(string newVariant, bool isVariant = true)
        {
            if (isVariant)
            {
                Variants.Add(newVariant);
            }
            else
            {
                TrueAnswers.Add(newVariant);
            }
        }

        public void RemoveVariant(bool isVariant = true)
        {
            if (isVariant)
            {
                int lastIndex = Variants.Count - 1;
                Variants.RemoveAt(lastIndex);
            }
            else
            {
                int lastIndex = TrueAnswers.Count - 1;
                TrueAnswers.RemoveAt(lastIndex);
            }
        }

        public void RemoveVariantByIndex(int index, bool isVariant = true)
        {
            if (isVariant)
            {
                Variants.RemoveAt(index);
            }
            else
            {
                TrueAnswers.RemoveAt(index);
            }
        }

        public void ClearVariants(bool isVariant = true)
        {
            if (isVariant)
            {
                Variants.Clear();
            }
            else
            {
                TrueAnswers.Clear();
            }
        }
    }
}
