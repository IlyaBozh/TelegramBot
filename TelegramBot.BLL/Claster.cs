using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.BL
{
    public class Claster
    {
        public string NameClaster { get; set; }

        public List<IQuestion> Questions { get; set; }
        public bool isHasAnswer;
        public Claster(string name, List<IQuestion> questions)
        {
            NameClaster = name;
            Questions = questions;
        }

        public void Add(string question)
        {
            Questions.Add(question);
        }

        public void Remove(int index)
        {
            Questions.RemoveAt(index);
        }

        public void Edit(string name, string newName)
        {
            if (newName == "" || newName == " ")
            {
                NameClaster = name;
            }
            else
            {
                NameClaster = newName;
            }
        }

        public void ClearQuestions(List<IQuestion> questions)
        {
            Questions.Clear();

        }


    }
}
