using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    public class SerialPacket
    {

        internal const int LengthFieldLength = 4;
        internal const int TypeFieldLength = 2;

        internal const string StartSequence = "W#L_";

        internal const string EndSequence = "_H^\n";

        public int DataLength { get; internal set; }

        public string Data { get; internal set; }

        public SerialPacketTypes PacketType { get; set; }

    }
}
