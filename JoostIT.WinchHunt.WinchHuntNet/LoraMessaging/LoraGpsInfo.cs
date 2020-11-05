using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraGpsInfo
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("sats")]
        public int Satellites { get; set; }

        [JsonProperty("spd")]
        public double Speed { get; set; }

        [JsonProperty("alt")]
        public double Altitude { get; set; }

        [JsonProperty("hdop")]
        public double Hdop { get; set; }

        [JsonProperty("fix")]
        public bool hasFix { get; set; }

    }
}
