using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL
{
    public class Claster
    {
        public string NameClaster { get; set; }

        public List<AbstractQuestion> Questions { get; set; }
        public bool isHasAnswer;

        public Claster(string name)
        {
            NameClaster = name;
        }
        public Claster(string name, List<AbstractQuestion> questions)
        {
            NameClaster = name;
            Questions = questions;
        }

        public void Add(AbstractQuestion question)
        {
            Questions.Add(question);
        }

        public void Remove(int index)
        {
            Questions.RemoveAt(index);
        }

        public void Edit(string name, string newName)
        {
            if (newName != "" || newName != " ")
            {
                NameClaster = newName;
            }
        }

        public void ClearQuestions(List<AbstractQuestion> questions)
        {
            Questions.Clear();
        }


    }
}
