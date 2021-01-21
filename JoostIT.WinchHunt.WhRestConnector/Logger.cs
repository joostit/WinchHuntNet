using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIt.WinchHunt.WhRestConnector
{
    public static class Logger
    {


        public static void Log(string msg)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"{TimeStamp()} : {msg}");
        }

        public static void Log()
        {
            Log("");
        }




        private static string TimeStamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


    }
}
