using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.HunterMessaging
{

    /// <summary>
    /// Builds a heartbeat packet from serial data
    /// </summary>
    internal class HeartbeatPacketBuilder
    {

        internal PacketParseResult<HeartbeatPacket> CreatePacket(String rawData)
        {
            PacketParseResult<HeartbeatPacket> result = null;

            try
            {
                HeartbeatPacket rxPacket = JsonConvert.DeserializeObject<HeartbeatPacket>(rawData);
                result = new PacketParseResult<HeartbeatPacket>(rxPacket);
            }
            catch (JsonException e)
            {
                return new PacketParseResult<HeartbeatPacket>(e);
            }


            return result;
        }

    }
}
