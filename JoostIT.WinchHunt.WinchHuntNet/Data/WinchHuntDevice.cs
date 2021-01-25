using JoostIT.WinchHunt.WinchHuntNet.Devices;
using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
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
        public DeviceInfo Device { get; set; }

        /// <summary>
        /// Gets the time stamp of the last update that was received from the device
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WinchHuntDevice()
        {

        }
        

        /// <summary>
        /// Call this method to indicate that information was updated and to update the time stamp.
        /// </summary>
        protected void SetLastUpdateNow()
        {
            LastUpdate = DateTime.Now;
        }


        /// <summary>
        /// Updates the device properties 
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="deviceType"></param>
        protected void UpdateDeviceProperties(LoraDeviceInfo deviceInfo, DeviceTypes deviceType)
        {
            if (Device == null)
            {
                Device = new DeviceInfo();
            }

            Device.DeviceType = deviceType;
            Device.Hardware = deviceInfo.Hardware;
            Device.Id = deviceInfo.Id;
            Device.Name = deviceInfo.Name;
            Device.Version = deviceInfo.Version;

            SetLastUpdateNow();
        }

    }
}
