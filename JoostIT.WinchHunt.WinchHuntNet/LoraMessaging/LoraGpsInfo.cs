using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    /// <summary>
    /// Defines a GPS Message as used by LoRa communication
    /// </summary>
    public class LoraGpsInfo
    {
        /// <summary>
        /// Gets or sets Latitude
        /// </summary>
        [JsonProperty("la")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets Longitude
        /// </summary>
        [JsonProperty("lo")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the number of sattelites
        /// </summary>
        [JsonProperty("s")]
        public int Satellites { get; set; }

        /// <summary>
        /// Gets or sets the speed
        /// </summary>
        [JsonProperty("v")]
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the altitude
        /// </summary>
        [JsonProperty("a")]
        public double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the HDOP value
        /// </summary>
        [JsonProperty("h")]
        public double Hdop { get; set; }

        /// <summary>
        /// Gets or sets whether a fix has been obtained
        /// </summary>
        [JsonProperty("f")]
        public bool hasFix { get; set; }

    }
}
