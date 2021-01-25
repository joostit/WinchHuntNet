using JoostIT.WinchHunt.WinchHuntNet.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.HunterMessaging
{
    internal class HeartbeatPacket
    {

        [JsonProperty("d")]
        public LoraDeviceInfo Device { get; set; }


        /// <summary>
        /// Returns a string representation of the state of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Device != null)
            {
                return $"Heart beat from {Device.Id} ({Device.Name})";
            }
            else
            {
                return base.ToString();
            }
        }

    }
}
