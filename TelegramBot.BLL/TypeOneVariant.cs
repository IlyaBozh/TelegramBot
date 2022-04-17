using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BLL
{
    public class TypeOneVariant : IQuestion
    {
        //public void Create(string question); в интерфейсе
        public TypeOneVariant(string question)
        {
            _question = question;
        }

        //public TypeOneVariant(string trueAnswer) : this(question)
        //{
        //    _trueAnswer = trueAnswer;
        //    isHasAnswer = true;
        //    _variant.Add(trueAnwer);
        //}

        //public TypeOneVariant(string userAnswer) : this(trueAnswer)
        //{
        //    _userAnswer = userAnswer;
        //    isHasAnswer = true;
        //    _variants.Add(userAnswer);
        //}

        private string _question;
        private List<string> _variants;
        private bool isHasAnswer = false;
        private bool isChecked;
        private string _trueAnswer;
        private string _userAnswer;

        public void Create(string question)
        {
            _question = question;
        }

        public void AddVariant(string variant)
        {
            _variants.Add(variant);
        }

        public int AddVariants(List<string> variants)
        {
            _variants = new List<string>();
            for (int i = 0; i < variants.Count; i++)
            {
                _variants[i] = variants[i].ToString();
            }
            int count = _variants.Count;
            return count;          
        }

        public void RemoveVariant(string variant)
        {
            if (_variants.Count == 0)
            {
                throw new ArgumentException("user's list is empty");
            }
            _variants!.Remove(variant);
        }

        public void Clear()
        {
            _variants.Clear();
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

        public bool Check()
        {
            if (_trueAnswer != _userAnswer)
            {
                isChecked = false;
                return false;
            }
            else
            {
                isChecked = true;
                return true;
            }
        }

        public void Show()
        {
            if (_variants.Count == 0)
            {
                throw new ArgumentException("user's list is empty");
            }
            foreach (string variant in _variants)
            {
                Console.WriteLine($"variant\n");
            }
        }
    }

    class Program
    {
        void Main(string[] args)
        {

        }
    }
}
