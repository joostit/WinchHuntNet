using JoostIT.WinchHunt.WinchHuntNet;
using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace JoostIT.WinchHunt.WhRestConnector
{
    class Program
    {
        static int Main(string[] args)
        {
            ConnectorApp app = new ConnectorApp();

            return app.Run(args);
        }



        

    }
}
