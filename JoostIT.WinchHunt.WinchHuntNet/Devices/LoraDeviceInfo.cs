using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Devices
{
    /// <summary>
    /// Defines Device information as used by LoRa communication
    /// </summary>
    public class LoraDeviceInfo
    {

        /// <summary>
        /// Gets or sets the device name
        /// </summary>
        [JsonProperty("n")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device ID
        /// </summary>
        [JsonProperty("i")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the device hardware ID
        /// </summary>
        [JsonProperty("hw")]
        public string Hardware { get; set; }

        /// <summary>
        /// Gets or sets the device version
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

    }
}
