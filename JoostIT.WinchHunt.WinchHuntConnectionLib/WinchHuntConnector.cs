using JoostIT.WinchHunt.WinchHuntConnectionLib.Data;
using JoostIT.WinchHunt.WinchHuntConnectionLib.LoraMessaging;
using JoostIT.WinchHunt.WinchHuntConnectionLib.SerialConnection;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib
{
    public class WinchHuntConnector : IDisposable
    {

        private const int BaudRate = 115200;
        private SerialPortConnector serialConnection;
        public List<string> GetAvailablePorts() => SerialPortConnector.GetAvailablePorts();
        private LoraPacketBuilder lorapacketBuilder = new LoraPacketBuilder();

        public WinchHuntConnector()
        {

        }

        ~WinchHuntConnector() => Dispose(false);

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

            // ToDo: Dispatch the Lora packet from here

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

    }
}
