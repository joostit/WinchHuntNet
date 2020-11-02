using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraPacketBuilder
    {

        private const int HexBase = 16;


        internal LoraPacket CreatePacket(String data)
        {
            LoraPacket retVal = new LoraPacket();

            if (data == null) { throw new ArgumentNullException("data"); }
            if (data.Length < 19) { throw new InvalidDataException($"Minimum data length is 19 characters. Got {data.Length} characters instead."); }

            string crcValueString = data.Substring(data.Length - 2, 2);
            retVal.Crc = Convert.ToByte(crcValueString, HexBase);

            string CrcDataString = data.Substring(0, data.Length - 2);
            byte calculatedCrc = Crc8.CalculateCrc(CrcDataString);

            if (retVal.Crc != calculatedCrc)
            {
                throw new InvalidDataException("CRC Mismatch");
            }

            retVal.SenderId = Convert.ToUInt32(data.Substring(7, 6), HexBase);
            retVal.MessageId = Convert.ToUInt16(data.Substring(13, 4), HexBase);
            retVal.JsonData = data.Substring(17, data.Length - 17 - 2);

            return retVal;


        }
    }
}
