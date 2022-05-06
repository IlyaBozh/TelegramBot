
using TelegramBot.BL.DataBase;

namespace TelegramBot.BL
{
    public class TestController
    {
        private TestsDataBase _testsDataBase;
        private UsersDataBase _usersDataBase;
        private static TestController _instance;

        private TestController()
        {
            _testsDataBase = TestsDataBase.GetInstance();
            _usersDataBase = UsersDataBase.GetInstance();
        }

        public static TestController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestController();
            }
            return _instance;
        }
    }
}
