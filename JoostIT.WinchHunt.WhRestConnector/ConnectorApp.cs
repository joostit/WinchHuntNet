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

            using (WinchHuntConnector connector = new WinchHuntConnector())
            {
                PrintWelcomeMessage(connector, config);

                connector.SerialDataRx += Connector_SerialDataRx;

                if (!String.IsNullOrWhiteSpace(config.ComPort))
                {
                    Logger.Log("Opening " + config.ComPort);
                    connector.Connect(config.ComPort);
                    Logger.Log($"Succesfully opened {config.ComPort}");
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


            Logger.Log($"   Configuration file  :   {configFile}");
            Logger.Log($"   Debug mode          :   {config.DebugMode}");
            Logger.Log($"   COM port            :   {comPort}");
            Logger.Log($"   REST URL            :   {restUrl}");
            Logger.Log($"   API Token           :   {apiKey}");
            Logger.Log($"   REST Update interval:   {config.UpdateInterval}s");
            Logger.Log("");
        }


        private void RunProgramLoop(WinchHuntConnector connector, WebRestClient restClient, AppConfiguration config)
        {

            FakeData fakeData = null;
            if (config.DebugMode)
            {
                fakeData = new FakeData();
            }

            while (true)
            {
                Thread.Sleep(config.UpdateInterval + 1000);

                if (config.ConnectToRest)
                {
                    UplinkPost post = new UplinkPost();
                    post.AccessToken = config.ApiAccessToken;
                    post.Hunter = connector.DeviceManager.Hunter;

                    post.Devices = new List<WinchFox>(connector.DeviceManager.Foxes.Values);
                    if (config.DebugMode)
                    {

                        fakeData.AddDebugFoxes(post.Devices);
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
                    Logger.Log($"RX: {e.ResultPackage.ToString()}");
                    break;

                default:
                    Logger.Log($"RX: Unsupported packet type: {e.Packet.PacketType}");
                    break;
            }
        }


        

    }
}
