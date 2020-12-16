using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.SerialConnection
{
    /// <summary>
    /// Defines different serial packet types
    /// </summary>
    public enum SerialPacketTypes
    {
        /// <summary>
        /// An invalid or undefined packet type
        /// </summary>
        Invalid,

        /// <summary>
        /// A Lora packet that was received
        /// </summary>
        LoraRx,

        /// <summary>
        /// A heartbeat signal
        /// </summary>
        HeartBeat

    }
}
