using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{
    /// <summary>
    /// Event argutments for Device related events
    /// </summary>
    public class DeviceEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the device info for the device that concerns this event
        /// </summary>
        public DeviceInfo Device { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device"></param>
        public DeviceEventArgs(DeviceInfo device)
        {
            this.Device = device;
        }

    }
}
