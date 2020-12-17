using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraPacketBuilder
    {

        private const int HexBase = 16;


        internal PacketParseResult<LoraPacket> CreatePacket(String data)
        {

            PacketParseResult<LoraPacket> retVal;
            LoraPacket packet = new LoraPacket();

            if (data == null) { throw new ArgumentNullException("data"); }

            try
            {
                if (data.Length < 19) { throw new InvalidDataException($"Minimum data length is 19 characters. Got {data.Length} characters instead."); }

                string crcValueString = data.Substring(data.Length - 2, 2);

                try
                {
                    packet.Crc = Convert.ToByte(crcValueString, HexBase);
                }
                catch (FormatException)
                {
                    throw new InvalidDataException("Could not read CRC bytes. Possibly caused by a malformed message");
                }

                string CrcDataString = data.Substring(0, data.Length - 2);
                byte calculatedCrc = Crc8.CalculateCrc(CrcDataString);

                if (packet.Crc != calculatedCrc)
                {
                    throw new InvalidDataException("CRC Mismatch");
                }

                packet.SenderId = Convert.ToUInt32(data.Substring(7, 6), HexBase);
                packet.MessageId = Convert.ToUInt16(data.Substring(13, 4), HexBase);
                packet.JsonData = data.Substring(17, data.Length - 17 - 2);

                retVal = new PacketParseResult<LoraPacket>(packet);
            }
            catch (InvalidDataException e)
            {
                retVal = new PacketParseResult<LoraPacket>(e);
            }
            catch (FormatException e)
            {
                retVal = new PacketParseResult<LoraPacket>(e);
            }
            

            return retVal;


        }
    }
}
