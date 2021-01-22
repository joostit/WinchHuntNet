using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraPacket
    {
        public const string MagicWord = "TZCWNCH";

        public uint SenderId { get; set; }

        public ushort MessageId { get; set; }

        public string JsonData { get; set; }

        public byte Crc { get; set; }

        public int Rssi { get; set; }

    }
}
