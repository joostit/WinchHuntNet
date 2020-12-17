using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using JoostIT.WinchHunt.WinchHuntNet.SerialConnection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Defines the Event Handler for the SerialDataReceived event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataRxEventHandler(object sender, DataRxEventArgs e);

    /// <summary>
    /// Event Arguments for receiving serial data
    /// </summary>
    public class DataRxEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the serial packet that was received
        /// </summary>
        public SerialPacket Packet { get; private set; }

        /// <summary>
        /// The ParseResult of this RX operation, if one was created
        /// </summary>
        public ParseResult ResultPackage { get; private set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public DataRxEventArgs(SerialPacket packet, ParseResult resultPackage)
        {
            this.Packet = packet;
            this.ResultPackage = resultPackage;
        }


    }
}
