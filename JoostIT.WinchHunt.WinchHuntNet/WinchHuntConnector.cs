using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using JoostIT.WinchHunt.WinchHuntNet.SerialConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Main utility class that connects to a WinchHunt device
    /// </summary>
    public class WinchHuntConnector : IDisposable
    {
        private bool isDisposed = false;
        private const int BaudRate = 115200;
        private SerialPortConnector serialConnection;
        private LoraPacketBuilder lorapacketBuilder = new LoraPacketBuilder();

        /// <summary>
        /// Gets the device manager that holds access to all known devices
        /// </summary>
        public DeviceManager DeviceManager { get; private set; } = new DeviceManager();        

        /// <summary>
        /// Gets or sets whether there's a live connection to the WincHunt device
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets raised after serial data has been received and processed internally
        /// </summary>
        public event DataRxEventHandler SerialDataRx;

        /// <summary>
        /// Connects to a WinchHunt device over the serial port
        /// </summary>
        /// <param name="portName">The port name to connect to. Note that this is case sensitive!</param>
        public void Connect(string portName)
        {

            if (isDisposed) { throw new ObjectDisposedException("WinchHuntConnector"); }
            if (serialConnection != null) { throw new InvalidOperationException("Cannot connect while there's already a connection."); }

            try
            {
                serialConnection = new SerialPortConnector();
                serialConnection.NewSerialPacket += SerialConnection_NewSerialPacket;
                serialConnection.PortClosedOnError += SerialConnection_PortClosedOnError;
                serialConnection.Connect(portName, BaudRate);
                IsConnected = serialConnection.IsOpen;
            }
            catch(Exception e)
            {
                serialConnection.Dispose();
                serialConnection = null;
                throw e;
            }
        }


        private void SerialConnection_PortClosedOnError(object sender, EventArgs e)
        {
            Console.WriteLine("Serial port closed unexpectedly.");

            
        }


        /// <summary>
        /// Gets a list of all available serial ports
        /// </summary>
        /// <returns>A list of strings that contain the serial port names</returns>
        public List<string> GetAvailablePorts()
        {
            return SerialPortConnector.GetAvailablePorts();
        }


        private void SerialConnection_NewSerialPacket(object sender, NewSerialPacketEventArgs e)
        {
            switch (e.Packet.PacketType)
            {
                
                case SerialPacketTypes.LoraRx:
                    PacketParseResult<FoxMessage> parseResult = ProcessLoraPacketReceived(e.Packet);
                    RaiseSerialDataReceived(e.Packet, parseResult);
                    break;
                case SerialPacketTypes.HeartBeat:
                    // ToDo: Process heartbeat signals
                    RaiseSerialDataReceived(e.Packet);
                    break;
                case SerialPacketTypes.Invalid:
                default:
                    throw new NotSupportedException($"Unsupported packet type: {e.Packet.PacketType}");
            }
            
        }


        private PacketParseResult<FoxMessage> ProcessLoraPacketReceived(SerialPacket serialPacket)
        {
            PacketParseResult<LoraPacket> parsedLoraPacket;
            PacketParseResult<FoxMessage> parsedFoxMessage;

            parsedLoraPacket = lorapacketBuilder.CreatePacket(serialPacket.Data);

            if (parsedLoraPacket.IsValid)
            {
                
                try
                {
                    FoxMessage message = JsonConvert.DeserializeObject<FoxMessage>(parsedLoraPacket.Result.JsonData);
                    DeviceManager.ProcessFoxMessage(message);
                    parsedFoxMessage = new PacketParseResult<FoxMessage>(message);
                }
                catch(JsonException e)
                {
                    parsedFoxMessage = new PacketParseResult<FoxMessage>(e);
                }
            }
            else
            {
                parsedFoxMessage = new PacketParseResult<FoxMessage>($"Exception while processing LoraPacket: \n{ parsedLoraPacket.ErrorMessage}");
            }

            return parsedFoxMessage;
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
                if (serialConnection != null)
                {
                    serialConnection.Dispose();
                }
            }
            isDisposed = true;
        }


        /// <summary>
        /// Constructor. Creates a new instance of this object
        /// </summary>
        public WinchHuntConnector()
        {
            
        }

        private void RaiseSerialDataReceived(SerialPacket packet, ParseResult message = null)
        {
            SerialDataRx?.Invoke(this, new DataRxEventArgs(packet, message));
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~WinchHuntConnector() => Dispose(false);



    }
}
