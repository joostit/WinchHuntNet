using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal class LoraDeviceInfo
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("hw")]
        public string Hardware { get; set; }

        [JsonPropertyName("v")]
        public int Version { get; set; }

    }
}
