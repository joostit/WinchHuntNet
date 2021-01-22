using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraPacketBuilder
    {

        private const int HexBase = 16;


        internal PacketParseResult<LoraPacket> CreatePacket(String rawData)
        {
            if (rawData == null) { throw new ArgumentNullException("rawData"); }

            LoraRxSerialPacket rxPacket; 

            try
            {
                rxPacket = JsonConvert.DeserializeObject<LoraRxSerialPacket>(rawData);
            }
            catch (JsonException e)
            {
                return new PacketParseResult<LoraPacket>(e);
            }

            PacketParseResult<LoraPacket> result = ConvertLoraRxToLoraPacket(rxPacket.Message);

            if (result.IsValid)
            {
                result.Result.Rssi = rxPacket.Rssi;
            }

            return result;

        }



        internal PacketParseResult<LoraPacket> ConvertLoraRxToLoraPacket(string rawLoraData)
        {
            LoraPacket packet = new LoraPacket();
            PacketParseResult<LoraPacket> retVal;

            try
            {
                if (rawLoraData.Length < 19) { throw new InvalidDataException($"Minimum data length is 19 characters. Got {rawLoraData.Length} characters instead."); }

                string crcValueString = rawLoraData.Substring(rawLoraData.Length - 2, 2);

                try
                {
                    packet.Crc = Convert.ToByte(crcValueString, HexBase);
                }
                catch (FormatException)
                {
                    throw new InvalidDataException("Could not read CRC bytes. Possibly caused by a malformed message");
                }

                string CrcDataString = rawLoraData.Substring(0, rawLoraData.Length - 2);
                byte calculatedCrc = Crc8.CalculateCrc(CrcDataString);

                if (packet.Crc != calculatedCrc)
                {
                    throw new InvalidDataException("CRC Mismatch");
                }

                packet.SenderId = Convert.ToUInt32(rawLoraData.Substring(7, 6), HexBase);
                packet.MessageId = Convert.ToUInt16(rawLoraData.Substring(13, 4), HexBase);
                packet.JsonData = rawLoraData.Substring(17, rawLoraData.Length - 17 - 2);

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
