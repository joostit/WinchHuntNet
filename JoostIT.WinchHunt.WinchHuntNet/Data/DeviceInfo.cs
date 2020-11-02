using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{
    public class DeviceInfo
    {

        public string Name { get; internal set; }

        public string Id { get; internal set; }

        public string Hardware { get; internal set; }

        public int Version { get; internal set; }

        public DeviceTypes DeviceType { get; internal set; }

        internal DeviceInfo()
        {

        }
    }
}
