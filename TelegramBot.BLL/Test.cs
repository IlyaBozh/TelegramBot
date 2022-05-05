using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL
{
    public class Test
    {
        public string Description { get; private set; }
        public List<Group> Groups { get; private set; }
        public List<AbstractQuestion> Questions { get; private set; }

        public Test()
        {

        }

        public Test(string description, List<Group> groups, List<AbstractQuestion> questions)
        {
            Description = description;
            Groups = groups;
            Questions = questions;
        }

        public void AddQuestion(AbstractQuestion newQuestion)
        {
            if (newQuestion is null)
            {
                throw new ArgumentException("there's no question");
            }
            Questions.Add(newQuestion);
        }

        public void DeleteQuestion(AbstractQuestion question)
        {
            if (question is null)
            {
                throw new ArgumentException("there's no question");
            }
            for (int i = 0; i < Questions.Count; i++)
            {
                if (Questions[i].Description == question.Description)
                {
                    Questions.RemoveAt(i);
                }
            }
        }



    }


}
