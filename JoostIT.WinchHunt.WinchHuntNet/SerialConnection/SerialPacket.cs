using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.SerialConnection
{
    internal class SerialPacket
    {

        public const int LengthFieldLength = 4;

        public const int TypeFieldLength = 2;

        public const string StartSequence = "W#L_";

        public const string EndSequence = "_H^\n";

        public int DataLength { get; internal set; }

        public string Data { get; internal set; }

        public SerialPacketTypes PacketType { get; set; }

    }
}
