using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.DataBase
{
    public class DataBase
    {
        public List<Group> UserGroups = new List<Group>();
        public List<Claster> Tests { get; set; }
        public List<Claster> Polls { get; set; }
        public List<AbstractQuestion> TestSingelQuestions { get; set; }
        public List<AbstractQuestion> TestSingelPolls { get; set; }

        public DataBase()
        { 
            UserGroups = new List<Group>();
            Tests = new List<Claster>();
            Polls = new List<Claster>();
            TestSingelQuestions = new List<AbstractQuestion>();
            TestSingelPolls = new List<AbstractQuestion>();
        }
    }
}
