using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoostIT.WinchHunt.WinchHuntNet.SerialConnection
{
    internal class SerialPortConnector : IDisposable
    {
        private bool isDisposed = false;
        private SerialPort port;

        private bool portStaysOpen = true;

        private SerialPacketBuilder packetBuilder = new SerialPacketBuilder();

        public event EventHandler<NewSerialPacketEventArgs> NewSerialPacket;

        public event EventHandler PortClosedOnError;


        public bool IsOpen
        {
            get
            {
                if(port != null)
                {
                    return port.IsOpen;
                }
                else
                {
                    return false;
                }
            }
        }

        internal static List<string> GetAvailablePorts()
        {
            return SerialPort.GetPortNames().ToList<string>();
        }


        internal void Connect(string portName, int baudrate)
        {

            if (isDisposed) { throw new ObjectDisposedException("WinchHuntConnector"); }
            if (port != null) { throw new InvalidOperationException("Cannot open the port after it has already been opened (or attempted)"); }

            if (!IsValidPort(portName))
            {
                Console.WriteLine($"Serial port '{portName}' does not exist.");
                ClosePort();
                return;
            }

            port = new SerialPort(portName, baudrate, Parity.None, 8);

            port.Open();

            if (port.IsOpen)
            {
                // Clear the exsisting data before attaching the listener
                port.ReadExisting();
                port.DataReceived += Port_DataReceived;
                port.ErrorReceived += Port_ErrorReceived;
                RunWatchdog();
            }
        }


        private void RunWatchdog()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {

                while (portStaysOpen)
                {
                    Thread.Sleep(1000);

                    if (!port.IsOpen)
                    {
                        ClosePort();
                    }
                }
            });
        }


        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ClosePort();
        }


        private void ClosePort()
        {
            portStaysOpen = false;
            RaisePortClosedOnError();
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


        /// <summary>
        /// Closes the connection and disposes all resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes all resources
        /// </summary>
        /// <param name="disposing">True if called from Dispose(). False when called from the finalizer</param>
        virtual protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(port!= null)
                {
                    port.Dispose();
                    port = null;
                }
            }
            isDisposed = true;
        }


        public SerialPortConnector()
        {
            packetBuilder.PacketReceived += PacketBuilder_PacketReceived;
        }


        ~SerialPortConnector() => Dispose(false);


        private void RaisePortClosedOnError()
        {
            PortClosedOnError?.Invoke(this, EventArgs.Empty);
        }
    }
}
