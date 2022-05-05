using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.BL.DataBase;

namespace TelegramBot.BL
{
    public class TestController
    {
        public Test UserTest { get; set; }
        public List<Group> TestGroup { get; set; } 
        private TestsDataBase _testsDataBase;
        private UsersDataBase _usersDataBase;
        private static TestController _instance;

        public TestController()
        {

        }

        public static TestController GetTestController()
        {
            if (_instance == null)
            {
                _instance = new TestController();
            }
            return _instance;
        }

        public static TestController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestController();
            }
            return _instance;
        }

        public void SetGroup(List<Group> groups)
        {
            _instance.TestGroup = groups;
        }

        public void SetTest(Test test)
        {
            _instance.UserTest = test;
        }

        public Dictionary<long, TestCounter> GetDictionary()
        {
            Dictionary<long, TestCounter> dictionary = new Dictionary<long, TestCounter>();
            for (int i = 0; i < _instance.TestGroup.Count; i++)
            {
                for (int j = 0; j < _instance.TestGroup[i].UserGroups.Count; j++)
                {
                    long chatId = _instance.TestGroup[i].UserGroups[j].Id;
                    dictionary.Add(chatId, new TestCounter(_instance.UserTest.Questions));
                }
            }
            return dictionary;
        }

    }
}
