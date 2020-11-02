using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.SerialConnection
{
    internal class SerialPortConnector : IDisposable
    {

        private SerialPort port;

        private SerialPacketBuilder packetBuilder = new SerialPacketBuilder();

        public event EventHandler<NewSerialPacketEventArgs> NewSerialPacket;


        internal static List<string> GetAvailablePorts()
        {
            return SerialPort.GetPortNames().ToList<string>();
        }


        internal void Connect(string portName, int baudrate)
        {

            if (port != null) { throw new InvalidOperationException("Cannot open the port after it has already been opened (or attempted)"); }

            if (!IsValidPort(portName))
            {
                throw new InvalidOperationException($"Serial port '{portName}' does not exist.");
            }

            port = new SerialPort(portName, baudrate, Parity.None, 8);

            port.Open();

            if (port.IsOpen)
            {
                // Clear the exsisting data before attaching the listener
                port.ReadExisting();
                port.DataReceived += Port_DataReceived;
            }
        }


        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadExisting();
            packetBuilder.ProcessSerialData(data);
        }


        private void PacketBuilder_PacketReceived(object sender, NewSerialPacketEventArgs e)
        {
            NewSerialPacket?.Invoke(this, e);
        }


        private bool IsValidPort(string portName)
        {
            return GetAvailablePorts().Contains(portName);
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
                if(port!= null)
                {
                    port.Dispose();
                }
            }
        }


        public SerialPortConnector()
        {
            packetBuilder.PacketReceived += PacketBuilder_PacketReceived;
        }


        ~SerialPortConnector() => Dispose(false);
    }
}
