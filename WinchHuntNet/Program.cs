using JoostIT.WinchHunt.HunterConnectionLib;
using System;
using System.Collections.Generic;

namespace WinchHuntNet
{
    class Program
    {
        static void Main(string[] args)
        {
            HunterConnector connector = new HunterConnector();
            List<String> ports = connector.GetAvailablePorts();
            connector.Connect("COM1", 9800);
        }
    }
}
