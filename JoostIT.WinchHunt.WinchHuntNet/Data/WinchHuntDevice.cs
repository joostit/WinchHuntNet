﻿using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{

    /// <summary>
    /// A base class that represents any WinchHunt device
    /// </summary>
    public abstract class WinchHuntDevice
    {

        /// <summary>
        /// Gets Device information
        /// </summary>
        public DeviceInfo Device { get; protected set; }

        /// <summary>
        /// Gets the time stamp of the last update that was received from the device
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        internal WinchHuntDevice()
        {

        }
        

        /// <summary>
        /// Call this method to indicate that information was updated and to update the time stamp.
        /// </summary>
        protected void SetLastUpdateNow()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
