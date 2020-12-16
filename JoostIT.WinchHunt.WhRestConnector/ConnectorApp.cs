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
                    Console.WriteLine(e.Message);
                    return -1;
                }

                Console.WriteLine("WinchHunt REST Connector");
                var ports = connector.GetAvailablePorts();
                Console.WriteLine("Available serial ports:");
                ports.ForEach((i) => Console.WriteLine("  " + i));
                Console.WriteLine();
                Console.WriteLine("Connecting to " + config.ComPort);


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
                    Console.WriteLine("Serial RX: Invalid packet type");
                    break;

                case WinchHuntNet.SerialConnection.SerialPacketTypes.LoraRx:
                    Console.WriteLine($"Serial RX: LoRaPacket from {e.Message.Device.Id} ({e.Message.Device.Name})");
                    break;

                case WinchHuntNet.SerialConnection.SerialPacketTypes.HeartBeat:
                    Console.WriteLine("Serial RX: Heartbeat");
                    break;

                default:
                    Console.WriteLine($"Serial RX: Unsupported packet type: {e.Packet.PacketType}");
                    break;
            }
        }

    }
}
