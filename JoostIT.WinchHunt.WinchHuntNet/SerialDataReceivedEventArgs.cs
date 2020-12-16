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
        /// Gets the packet that was received
        /// </summary>
        public SerialPacket Packet { get; private set; }

        /// <summary>
        /// If The packet type was a lora message, this property gets that message
        /// </summary>
        public FoxMessage Message { get; private set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public DataRxEventArgs(SerialPacket packet, FoxMessage message)
        {
            this.Packet = packet;
            this.Message = message;
        }


    }
}
