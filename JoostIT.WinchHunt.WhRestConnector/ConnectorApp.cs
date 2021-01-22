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
                    AppConfigurationLoader loader = new AppConfigurationLoader();

                    config = loader.LoadConfiguration(args);
                }
                catch (InvalidDataException e)
                {
                    Logger.Log(e.Message);
                    return -1;
                }

                PrintWelcomeMessage(connector, config);

                connector.SerialDataRx += Connector_SerialDataRx;

                if (!String.IsNullOrWhiteSpace(config.ComPort))
                {
                    Logger.Log("Connecting to " + config.ComPort);
                    connector.Connect(config.ComPort);
                }

                WebRestClient restClient = new WebRestClient(config);

                RunProgramLoop(connector, restClient, config);
            }

            return 0;
        }


        private void PrintWelcomeMessage(WinchHuntConnector connector, AppConfiguration config)
        {
            Logger.Log("WinchHunt REST Connector");
            Logger.Log("(c) 2020-2021 Joost Haverkort");
            Logger.Log();
            PrintConfig(config);

            var ports = connector.GetAvailablePorts();
            Logger.Log("Available serial ports:");
            ports.ForEach((i) => Logger.Log("   " + i));
            Logger.Log();
        }


        private void PrintConfig(AppConfiguration config)
        {
            string configFile = "";
            string debugMode;
            string comPort = "";
            string apiKey = "";
            string restUrl = "";

            Logger.Log("Configuration: ");
            if (!String.IsNullOrEmpty(config.ConfigurationFile))
            {
                configFile = config.ConfigurationFile;
            }

            if (!String.IsNullOrEmpty(config.ComPort))
            {
                comPort = config.ComPort;
            }

            if (!String.IsNullOrEmpty(config.ApiAccessToken))
            {
                apiKey = config.ApiAccessToken;
            }

            if (!String.IsNullOrEmpty(config.RestUrl))
            {
                restUrl = config.RestUrl;
            }

            debugMode = config.DebugMode.ToString();


            Logger.Log($"   Configuration file  :   {configFile}");
            Logger.Log($"   Debug mode          :   {debugMode}");
            Logger.Log($"   COM port            :   {comPort}");
            Logger.Log($"   REST URL            :   {restUrl}");
            Logger.Log($"   API Token           :   {apiKey}");
            Logger.Log("");
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
                    if (config.DebugMode)
                    {
                        AddDebugFoxes(post.Devices);
                    }

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


        private static void AddDebugFoxes(List<WinchFox> retVal)
        {

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "112233",
                        Name = "FakeFox1",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.278721,
                        Longitude = 6.898578
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "AABBCC",
                        Name = "FakeFox2",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.278655,
                        Longitude = 6.898686
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "55EE66",
                        Name = "FakeFox3",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.271880,
                        Longitude = 6.882657
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "77FF88",
                        Name = "FakeFox4",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.275661,
                        Longitude = 6.894856
                    },
                    LastUpdate = DateTime.UtcNow
                });
        }

    }
}
