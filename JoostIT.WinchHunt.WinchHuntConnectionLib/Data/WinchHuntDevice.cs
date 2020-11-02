using JoostIT.WinchHunt.WinchHuntConnectionLib.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.Data
{
    public abstract class WinchHuntDevice
    {
        public DeviceInfo Device { get; protected set; }

        public DateTime LastUpdate { get; private set; }

        internal WinchHuntDevice()
        {

        }
        
        protected void SetLastUpdateNow()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
