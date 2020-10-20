using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.HunterConnectionLib.Messaging
{
    public class FoxMessage
    {
        [JsonPropertyName("dev")]
        public DeviceInfo Device { get; set; }

        [JsonPropertyName("gps")]
        public GpsInfo Gps { get; set; }
    }
}
