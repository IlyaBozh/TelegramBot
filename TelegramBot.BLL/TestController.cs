
using TelegramBot.BL.DataBase;

namespace TelegramBot.BL
{
    public class TestController
    {
        public List<ClasterQuestions> Questions { get; set; }

        public long Id { get; set; }

        public int Counter { get; set; }

        public int IndexClaster { get; set; }

        public TestController(long id)
        {
            Questions = new List<ClasterQuestions>();
            Id = id;
            Counter = 0;
            IndexClaster = -1;
        }

        public void setClasterIndex()
        {
            IndexClaster += 1;
        }

    }
}
