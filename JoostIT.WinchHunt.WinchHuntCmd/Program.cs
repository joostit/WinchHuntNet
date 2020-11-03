using JoostIT.WinchHunt.WinchHuntNet;
using System;

namespace JoostIT.WinchHunt.WinchHuntCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WinchHuntConnector connector = new WinchHuntConnector())
            {
                var ports = connector.GetAvailablePorts();

                Console.WriteLine("WinchHuntConnector.");
                Console.WriteLine("Available serial ports:");

                ports.ForEach((i) => Console.WriteLine(i));

                connector.Connect("com6");


                Console.ReadKey();
            }
        }
    }
}
