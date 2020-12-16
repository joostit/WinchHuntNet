using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraGpsInfo
    {
        [JsonProperty("la")]
        public double Latitude { get; set; }

        [JsonProperty("lo")]
        public double Longitude { get; set; }

        [JsonProperty("s")]
        public int Satellites { get; set; }

        [JsonProperty("v")]
        public double Speed { get; set; }

        [JsonProperty("a")]
        public double Altitude { get; set; }

        [JsonProperty("h")]
        public double Hdop { get; set; }

        [JsonProperty("f")]
        public bool hasFix { get; set; }

    }
}
