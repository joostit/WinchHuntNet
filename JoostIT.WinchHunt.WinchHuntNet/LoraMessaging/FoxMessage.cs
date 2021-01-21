using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{

    /// <summary>
    /// Defines a Fox Message as used by LoRa communication
    /// </summary>
    public class FoxMessage
    {

        /// <summary>
        /// Gets or sets the Device information
        /// </summary>
        [JsonProperty("d")]
        public LoraDeviceInfo Device { get; set; }

        /// <summary>
        /// Gets or sets GPS information
        /// </summary>
        [JsonProperty("g")]
        public LoraGpsInfo Gps { get; set; }


        /// <summary>
        /// Returns a string representation of the state of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Device != null && Gps != null)
            {
                return $"Fox {Device.Id} ({Device.Name}). Gps Fix: {Gps.hasFix} - Sats: {Gps.Satellites} - Speed: {Gps.Speed} - ({Gps.Latitude}, {Gps.Longitude})";
            }
            else
            {
                return base.ToString();
            }
        }
    }
}
