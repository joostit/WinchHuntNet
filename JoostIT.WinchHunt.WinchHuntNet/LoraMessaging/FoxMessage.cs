using JoostIT.WinchHunt.WinchHuntNet.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Gets or sets the RSSI at which this message was received.
        /// Note that this is not included in the LoRa message sent by the fox itself, but added later.
        /// </summary>
        [JsonIgnore]
        public int Rssi { get; set; }


        /// <summary>
        /// Returns a string representation of the state of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Device != null && Gps != null)
            {
                return $"Fox {Device.Id} ({Device.Name}) @{Rssi}dB. Gps Fix: {Gps.hasFix} - Sats: {Gps.Satellites} - Speed: {Gps.Speed} - Pos: {Gps.Latitude}, {Gps.Longitude}";
            }
            else
            {
                return base.ToString();
            }
        }
    }
}
