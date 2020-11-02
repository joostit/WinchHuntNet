using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.Data
{
    public class DeviceEventArgs : EventArgs
    {

        public DeviceInfo Device { get; private set; }

        public DeviceEventArgs(DeviceInfo device)
        {
            this.Device = device;
        }

    }
}
