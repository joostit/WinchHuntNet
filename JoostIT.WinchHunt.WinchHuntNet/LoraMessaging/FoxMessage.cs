using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class FoxMessage
    {
        [JsonPropertyName("dev")]
        public LoraDeviceInfo Device { get; set; }

        [JsonPropertyName("gps")]
        public LoraGpsInfo Gps { get; set; }
    }
}
