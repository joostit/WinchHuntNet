using JoostIT.WinchHunt.WinchHuntNet;
using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Threading;

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

                connector.Connect("COM6");

                WebRestClient restClient = new WebRestClient();

                while (true)
                {
                    Thread.Sleep(3000);
                    FoxPost post = new FoxPost();
                    post.Devices = new List<WinchFox>(connector.DeviceManager.Foxes.Values);

                    restClient.sendFoxes(post);
                }



                
            }
        }


    }
}
