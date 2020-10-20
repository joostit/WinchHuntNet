using JoostIT.WinchHunt.HunterConnectionLib.SerialConnection;
using System;
using System.Collections.Generic;

namespace JoostIT.WinchHunt.HunterConnectionLib
{
    public class HunterConnector : IDisposable
    {


        ~HunterConnector() => Dispose(false);


        SerialPortConnector serialConnection = new SerialPortConnector();


        public List<string> GetAvailablePorts() => serialConnection.GetAvailablePorts();


        public void Connect(string portName, int baudrate)
        {
            serialConnection.Connect(portName, baudrate);
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
                serialConnection.Dispose();
            }
        }

    }
}
