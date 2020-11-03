using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{

    /// <summary>
    /// Holds information about a device
    /// </summary>
    public class DeviceInfo
    {

        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The device ID
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The device Hardware
        /// </summary>
        public string Hardware { get; internal set; }

        /// <summary>
        /// The device Version
        /// </summary>
        public int Version { get; internal set; }

        /// <summary>
        /// The type of device
        /// </summary>
        public DeviceTypes DeviceType { get; internal set; }

        internal DeviceInfo()
        {

        }
    }
}
