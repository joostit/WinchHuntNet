using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.Data
{
    public class GpsInfo
    {

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("sats")]
        public int Satellites { get; set; }

        [JsonPropertyName("spd")]
        public double Speed { get; set; }

        [JsonPropertyName("alt")]
        public double Altitude { get; set; }

        [JsonPropertyName("hdop")]
        public double Hdop { get; set; }

        [JsonPropertyName("fix")]
        public bool hasFix { get; set; }

    }
}
