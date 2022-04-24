using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelegramBot.BL.Questions
{
    public class TypeSeveralVariants : AbstractQuestion
    {


        public string Question { get; set; }
        public List<string> Variants { get; set; }
        private List<string> _trueAnswers { get; set; }
        public List<string> UserAnswers { get; set; }
        private bool isHasAnswer = false;


        public TypeSeveralVariants(string question, List<string> variants)
        {
            Question = question;
            Variants = variants;
        }


        public TypeSeveralVariants(string question, List<string> trueAnswers, List<string> variants)
        {
            Question = question;
            Variants = variants;
            _trueAnswers = trueAnswers;
            isHasAnswer = true;
        }


        public void AddVariant(string variant)
        {
            Variants.Add(variant);
        }


        public void AddTrueAnswer(string trueAnswer)
        {
            _trueAnswers.Add(trueAnswer);
        }


        public void EditQuestion(string newQuestion)
        {
            Question = newQuestion;
        }


        public void EditVariant(string variant, string newVariant)
        {
            if (Variants == null)
            {
                throw new Exception("list of variants is empty");
            }
            for (int i = 0; i < Variants.Count; i++)
            {
                if (Variants[i] == variant)
                {
                    Variants[i] = newVariant;
                    break;
                }
            }
        }


        public void EditTrueAnswer(string trueAnswer, string newTrueAnswer)
        {
            if (_trueAnswers == null)
            {
                throw new Exception("list of variants is empty");
            }
            for (int i = 0; i < _trueAnswers.Count; i++)
            {
                if (_trueAnswers[i] == trueAnswer)
                {
                    _trueAnswers[i] = newTrueAnswer;
                    break;
                }
            }
        }


        public void RemoveVariant(string variant)
        {
            if (Variants.Count == 0)
            {
                throw new ArgumentException("user's list is empty");
            }
            Variants!.Remove(variant);
        }


        public void RemoveTrueAnswer(string trueAnswer)
        {
            if (_trueAnswers.Count == 0)
            {
                throw new ArgumentException("user's list is empty");
            }
            _trueAnswers!.Remove(trueAnswer);
        }


        public void ClearVariants()
        {
            Variants.Clear();
        }


        public void ClearTrueAnswers()
        {
            _trueAnswers.Clear();
        }








        public bool Check()
        {
            throw new NotImplementedException();
        }
    }
}

