using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraRxSerialPacket
    {
        [JsonProperty("msg")]
        public String Message { get; set; }

        [JsonProperty("rssi")]
        public int Rssi { get; set; }

    }
}
