using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class TypeOneVariant : IQuestion
    {
        public TypeOneVariant(string question)
        {
            Question = question;
        }

        public TypeOneVariant(string question, string trueAnswer)
        {
            Question = question;
            TrueAnswer = trueAnswer;
            //isHasAnswer = true;
            //Variants.Add(trueAnswer);
        }

        public string Question { get; set; }
        public List<string> Variants { get; set; }
        //private bool isHasAnswer = false;
        //private bool isChecked;
        public string TrueAnswer { get; set; }
        public string userAnswer { get; set; }

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
                if(Variants[i] == variant)
                {
                    Variants[i] = newVariant;
                    break;
                }                
            }
        }

        public void AddVariant(string variant)
        {
            Variants.Add(variant);
        }

        //public int AddVariants(List<string> variants)
        //{
        //    _variants = new List<string>();
        //    for (int i = 0; i < variants.Count; i++)
        //    {
        //        _variants[i] = variants[i].ToString();
        //    }
        //    int count = _variants.Count;
        //    return count;          
        //}

        public void RemoveVariant(string variant)
        {
            if (Variants.Count == 0)
            {
                throw new ArgumentException("user's list is empty");
            }
            Variants!.Remove(variant);
        }

        public void Clear()
        {
            Variants.Clear();
        }

        public bool Check()
        {
            throw new NotImplementedException();
        }

        //public void SetUserAnswer(string userAnswer)
        //{
        //    if(isHasAnswer is false)
        //    {
        //        throw new ArgumentException("it's a pool without true answer");
        //    }
        //    _userAnswer = userAnswer;
        //    _variants.Add(_userAnswer);
        //}

        //public bool Check()
        //{
        //    if (_trueAnswer != userAnswer)
        //    {
        //        isChecked = false;
        //        return false;
        //    }
        //    else
        //    {
        //        isChecked = true;
        //        return true;
        //    }
        //}

        //public void Show()
        //{
        //    if (Variants.Count == 0)
        //    {
        //        throw new ArgumentException("user's list is empty");
        //    }
        //    foreach (string variant in Variants)
        //    {
        //        Console.WriteLine($"variant\n");
        //    }
        //}
    }
}
