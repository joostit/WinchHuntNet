using JoostIT.WinchHunt.WinchHuntNet.HunterMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{
    /// <summary>
    /// Represents a Hunter device. This might be the device that receives all LoRa signals.
    /// </summary>
    public class WinchHunter : WinchHuntDevice
    {

        /// <summary>
        /// Creates an empty WinchHunter device
        /// </summary>
        public WinchHunter()
        {

        }

        internal WinchHunter(HeartbeatPacket packet)
        {
            ApplyNewData(packet);
        }


        internal void UpdateData(HeartbeatPacket packet)
        {
            base.UpdateDeviceProperties(packet.Device, DeviceTypes.Hunter);

        }


        private void ApplyNewData(HeartbeatPacket message)
        {
            base.UpdateDeviceProperties(message.Device, DeviceTypes.Hunter);
        }


    }
}
