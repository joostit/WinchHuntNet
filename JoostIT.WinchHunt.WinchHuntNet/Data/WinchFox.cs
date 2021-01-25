using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{

    /// <summary>
    /// Represents a Fox device in the WinchHunt system
    /// </summary>
    public class WinchFox : WinchHuntDevice
    {

        /// <summary>
        /// Gets last known GPS state information
        /// </summary>
        public GpsInfo Gps { get; set; } = new GpsInfo();

        /// <summary>
        /// Gets or sets the last RSSI at which this Fox was received
        /// </summary>
        public int LastRssi { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WinchFox()
        {
        }


        internal WinchFox(FoxMessage message)
        {
            Gps = new GpsInfo();
            UpdateDeviceProperties(message.Device, DeviceTypes.Fox);
            Update(message);
        }


        private void ApplyNewData(FoxMessage message)
        {
            Update(message);
        }



        internal void Update(FoxMessage message)
        {
            Gps.Altitude = message.Gps.Altitude;
            Gps.HasFix = message.Gps.hasFix;
            Gps.Hdop = message.Gps.Hdop;
            Gps.Latitude = message.Gps.Latitude;
            Gps.Longitude = message.Gps.Longitude;
            Gps.Satellites = message.Gps.Satellites;
            Gps.Speed = message.Gps.Speed;

            LastRssi = message.Rssi;

            SetLastUpdateNow();
        }


        

    }
}
