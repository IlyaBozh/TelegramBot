using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.Questions;

namespace TelegramBot.BL.DataBase
{
    public class TestsDataBase
    {
        public List<Claster> Tests { get; set; }
        public List<Claster> Polls { get; set; }
        public List<AbstractQuestion> TestSingelQuestions { get; set; }
        public List<AbstractQuestion> TestSingelPolls { get; set; }
        private static TestsDataBase _instance;

        private TestsDataBase()
        {  
            Tests = new List<Claster>();
            Polls = new List<Claster>();
            TestSingelQuestions = new List<AbstractQuestion>();
            TestSingelPolls = new List<AbstractQuestion>();
        }

        public static TestsDataBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestsDataBase();
            }
            return _instance;
        }
    }
}
