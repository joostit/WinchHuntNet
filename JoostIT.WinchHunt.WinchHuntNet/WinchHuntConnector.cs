using JoostIT.WinchHunt.WinchHuntNet.HunterMessaging;
using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using JoostIT.WinchHunt.WinchHuntNet.SerialConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Main utility class that connects to a WinchHunt device
    /// </summary>
    public class WinchHuntConnector : IDisposable
    {
        private string portName = "";
        private bool isDisposed = false;
        private const int BaudRate = 115200;
        private SerialPortConnector serialConnection;
        private LoraPacketBuilder lorapacketBuilder = new LoraPacketBuilder();
        private HeartbeatPacketBuilder heartbeatpacketBuilder = new HeartbeatPacketBuilder();

        private bool tryReconnect = true;
        private int autoReconnectTimeout = 5000;

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
            if (serialConnection != null) { throw new InvalidOperationException($"Cannot open {portName} while there's already an existing SerialConnection object."); }

            this.portName = portName;

            try
            {
                serialConnection = new SerialPortConnector();
                serialConnection.NewSerialPacket += SerialConnection_NewSerialPacket;
                serialConnection.PortClosedOnError += SerialConnection_PortClosedOnError;
                serialConnection.Connect(portName, BaudRate);
                IsConnected = serialConnection.IsOpen;

                if (IsConnected)
                {
                    //Console.WriteLine($"{portName} opened sucessfully");
                }
                else
                {
                    serialConnection.Dispose();
                    serialConnection = null;
                    WaitAndReconnect();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Could not open serial port: " + e.Message.ToString());
                serialConnection.Dispose();
                serialConnection = null;
                WaitAndReconnect();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Could not open serial port: " + e.Message.ToString());
                serialConnection.Dispose();
                serialConnection = null;
                WaitAndReconnect();
            }
            catch (Exception e)
            {
                serialConnection.Dispose();
                serialConnection = null;
                throw e;
            }
        }


        private void SerialConnection_PortClosedOnError(object sender, EventArgs e)
        {
            IsConnected = false;

            if (serialConnection != null)
            {
                serialConnection.Dispose();
                serialConnection = null;
            }

            WaitAndReconnect();
        }


        private void WaitAndReconnect()
        {
            if (tryReconnect)
            {

                //Console.WriteLine($"Trying to reopen in {autoReconnectTimeout / 1000} s");
                Thread.Sleep(autoReconnectTimeout);

                // Be sure that the value hasn't changed while we were waiting
                if (tryReconnect)
                {
                    Connect(portName);
                }
            }
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
                    PacketParseResult<FoxMessage> foxResult = ProcessLoraPacketReceived(e.Packet);
                    RaiseSerialDataReceived(e.Packet, foxResult);
                    break;
                case SerialPacketTypes.HeartBeat:
                    PacketParseResult<HeartbeatPacket> hbResult = ProcessHeartbeatPacketReceived(e.Packet);
                    RaiseSerialDataReceived(e.Packet, hbResult);
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
                    message.Rssi = parsedLoraPacket.Result.Rssi;
                    DeviceManager.ProcessFoxMessage(message);
                    parsedFoxMessage = new PacketParseResult<FoxMessage>(message);
                }
                catch (JsonException e)
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


        private PacketParseResult<HeartbeatPacket> ProcessHeartbeatPacketReceived(SerialPacket serialPacket)
        {
            PacketParseResult<HeartbeatPacket> result = null;

            result = heartbeatpacketBuilder.CreatePacket(serialPacket.Data);
            if (result.IsValid)
            {
                DeviceManager.ProcessHeartbeatMessage(result.Result);
            }

            return result;
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
            tryReconnect = false;
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
