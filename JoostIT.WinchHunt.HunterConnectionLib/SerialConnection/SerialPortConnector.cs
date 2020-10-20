using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    internal class SerialPortConnector : IDisposable
    {


        internal List<string> GetAvailablePorts()
        {
            return SerialPort.GetPortNames().ToList<string>();
        }

        internal void Connect(string portName, int baudrate)
        {
            if (!IsValidPort(portName))
            {
                throw new InvalidOperationException($"Serial port '{portName}' does not exist.");
            }
        }


        private bool IsValidPort(string portName)
        {
            // ToDo: Validate if this should be case insensitive?
            return GetAvailablePorts().Contains(portName);
        }



        ~SerialPortConnector() => Dispose(false);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        virtual protected void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}
