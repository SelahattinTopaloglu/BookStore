using System;

namespace BusinessLogic.CCS
{
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("veritabanına loglandı");
        }
    }
}
