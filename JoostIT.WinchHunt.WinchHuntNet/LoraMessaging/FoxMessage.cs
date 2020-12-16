using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class FoxMessage
    {
        [JsonProperty("d")]
        public LoraDeviceInfo Device { get; set; }

        [JsonProperty("g")]
        public LoraGpsInfo Gps { get; set; }
    }
}
