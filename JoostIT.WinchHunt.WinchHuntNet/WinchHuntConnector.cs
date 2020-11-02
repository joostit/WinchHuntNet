using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using JoostIT.WinchHunt.WinchHuntNet.SerialConnection;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JoostIT.WinchHunt.WinchHuntNet
{
    public class WinchHuntConnector : IDisposable
    {

        private const int BaudRate = 115200;
        private SerialPortConnector serialConnection;
        private LoraPacketBuilder lorapacketBuilder = new LoraPacketBuilder();


        public DeviceManager DeviceManager { get; private set; } = new DeviceManager();

        
        public void Connect(string portName)
        {

            if (serialConnection != null) { throw new InvalidOperationException("Cannot connect while there's already a connection."); }

            try
            {
                serialConnection = new SerialPortConnector();
                serialConnection.NewSerialPacket += SerialConnection_NewSerialPacket;
                serialConnection.Connect(portName, BaudRate);
            }
            catch(Exception e)
            {
                serialConnection.Dispose();
                serialConnection = null;
                throw e;
            }
        }

        public List<string> GetAvailablePorts()
        {
            return SerialPortConnector.GetAvailablePorts();
        }

        private void SerialConnection_NewSerialPacket(object sender, NewSerialPacketEventArgs e)
        {
            switch (e.Packet.PacketType)
            {
                
                case SerialPacketTypes.LoraRx:
                    ProcessLoraPacketReceived(e.Packet);
                    break;
                case SerialPacketTypes.HeartBeat:
                    break;
                case SerialPacketTypes.Invalid:
                default:
                    throw new NotSupportedException($"Unsupported packet type: {e.Packet.PacketType}");
            }
        }


        private void ProcessLoraPacketReceived(SerialPacket serialPacket)
        {
            LoraPacket loraPacket = lorapacketBuilder.CreatePacket(serialPacket.Data);

            FoxMessage message = JsonSerializer.Deserialize<FoxMessage>(loraPacket.JsonData);

            DeviceManager.ProcessFoxMessage(message);
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        virtual protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (serialConnection != null)
                {
                    serialConnection.Dispose();
                }
            }
        }


        public WinchHuntConnector()
        {
            DeviceManager.FoxAdded += DeviceManager_FoxAdded;
            DeviceManager.FoxUpdated += DeviceManager_FoxUpdated;
            DeviceManager.FoxRemoved += DeviceManager_FoxRemoved;
        }

        private void DeviceManager_FoxRemoved(object sender, Data.DeviceEventArgs e)
        {
            Console.WriteLine($"Fox Removed: {e.Device.Id}");
        }

        private void DeviceManager_FoxUpdated(object sender, Data.DeviceEventArgs e)
        {
            Console.WriteLine($"Fox Updated: {e.Device.Id}");
        }

        private void DeviceManager_FoxAdded(object sender, Data.DeviceEventArgs e)
        {
            Console.WriteLine($"Fox Added: {e.Device.Id}");
        }

        ~WinchHuntConnector() => Dispose(false);



    }
}
