using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.SerialConnection
{

    /// <summary>
    /// Represents a serial data packet
    /// </summary>
    public class SerialPacket
    {

        /// <summary>
        /// Defines the length of the [Length] field
        /// </summary>
        public const int LengthFieldLength = 4;

        /// <summary>
        /// Defines the length of the [Type] field
        /// </summary>
        public const int TypeFieldLength = 2;

        /// <summary>
        /// Defines the Start sequence
        /// </summary>
        public const string StartSequence = "W#L_";

        /// <summary>
        /// Defines the end sequence
        /// </summary>
        public const string EndSequence = "_H^\n";

        /// <summary>
        /// Gets or sets the data length
        /// </summary>
        public int DataLength { get; internal set; }

        /// <summary>
        /// Gets or sets the actual data part
        /// </summary>
        public string Data { get; internal set; }

        /// <summary>
        /// Gets or sets the packet type
        /// </summary>
        public SerialPacketTypes PacketType { get; internal set; }

    }
}
