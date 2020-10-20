using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.SerialConnection
{
    internal class NewSerialPacketEventArgs
    {

        public SerialPacket Packet { get; set; }

        public NewSerialPacketEventArgs(SerialPacket packet)
        {
            this.Packet = packet;
        }

    }
}
