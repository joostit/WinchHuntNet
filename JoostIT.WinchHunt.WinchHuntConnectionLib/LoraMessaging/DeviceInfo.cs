using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.LoraMessaging
{
    public class DeviceInfo
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("hw")]
        public double Hardware { get; set; }

        [JsonPropertyName("v")]
        public int Version { get; set; }

    }
}
