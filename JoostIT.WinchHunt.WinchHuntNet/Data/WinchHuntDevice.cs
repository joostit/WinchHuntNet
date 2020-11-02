using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
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
