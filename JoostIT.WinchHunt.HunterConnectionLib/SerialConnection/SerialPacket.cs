using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    public class SerialPacket
    {

        public const string StartSequence = "W#L_";

        public const string EndSequence = "_H^\n";

        public int DataLength { get; set; }

        public string Data { get; set; }

    }
}
