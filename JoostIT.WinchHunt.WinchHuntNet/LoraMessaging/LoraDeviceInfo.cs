using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraDeviceInfo
    {

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("i")]
        public string Id { get; set; }

        [JsonProperty("hw")]
        public string Hardware { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }

    }
}
