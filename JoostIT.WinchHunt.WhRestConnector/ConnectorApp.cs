using JoostIt.WinchHunt.WhRestConnector;
using JoostIT.WinchHunt.WhRestConnector;
using JoostIT.WinchHunt.WinchHuntNet;
using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace JoostIT.WinchHunt.WhRestConnector
{
    class ConnectorApp
    {


        public int Run(string[] args)
        {

            using (WinchHuntConnector connector = new WinchHuntConnector())
            {

                AppConfiguration config;

                try
                {
                    config = new AppConfiguration(args);
                }
                catch (InvalidDataException e)
                {
                    Logger.Log(e.Message);
                    return -1;
                }

                Logger.Log("WinchHunt REST Connector");
                Logger.Log("(c) 2020-2021 Joost Haverkort");
                Logger.Log();
                var ports = connector.GetAvailablePorts();
                Logger.Log("Available serial ports:");
                ports.ForEach((i) => Logger.Log("  " + i));
                Logger.Log();
                Logger.Log("Connecting to " + config.ComPort);


                connector.SerialDataRx += Connector_SerialDataRx;
                connector.Connect(config.ComPort);

                WebRestClient restClient = new WebRestClient(config);

                RunProgramLoop(connector, restClient, config);
            }

            return 0;
        }


        private void RunProgramLoop(WinchHuntConnector connector, WebRestClient restClient, AppConfiguration config)
        {
            while (true)
            {
                Thread.Sleep(3000);

                if (config.ConnectToRest)
                {
                    FoxPost post = new FoxPost();
                    post.AccessToken = config.ApiAccessToken;
                    post.Devices = new List<WinchFox>(connector.DeviceManager.Foxes.Values);

                    restClient.sendFoxes(post);
                }
            }
        }



        private void Connector_SerialDataRx(object sender, DataRxEventArgs e)
        {
            switch (e.Packet.PacketType)
            {
                case WinchHuntNet.SerialConnection.SerialPacketTypes.Invalid:
                    Logger.Log("RX: Invalid packet type");
                    break;

                case WinchHuntNet.SerialConnection.SerialPacketTypes.LoraRx:
                    Logger.Log($"RX: {e.ResultPackage.ToString()}");
                    break;

                case WinchHuntNet.SerialConnection.SerialPacketTypes.HeartBeat:
                    Logger.Log("RX: Heartbeat");
                    break;

                default:
                    Logger.Log($"RX: Unsupported packet type: {e.Packet.PacketType}");
                    break;
            }
        }

    }
}
