using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{
    public class WinchFox : WinchHuntDevice
    {

        public GpsInfo Gps { get; private set; }


        internal WinchFox(FoxMessage message)
        {
            Gps = new GpsInfo();
            CopyDeviceProperties(message);
            UpdateGpsData(message);
        }


        private void ApplyNewData(FoxMessage message)
        {
            UpdateGpsData(message);
        }



        internal void UpdateGpsData(FoxMessage message)
        {
            Gps.Altitude = message.Gps.Altitude;
            Gps.hasFix = message.Gps.hasFix;
            Gps.Hdop = message.Gps.Hdop;
            Gps.Latitude = message.Gps.Latitude;
            Gps.Longitude = message.Gps.Longitude;
            Gps.Satellites = message.Gps.Satellites;
            Gps.Speed = message.Gps.Speed;

            SetLastUpdateNow();
        }


        private void CopyDeviceProperties(FoxMessage message)
        {
            Device = new DeviceInfo();
            Device.DeviceType = DeviceTypes.Fox;
            Device.Hardware = message.Device.Hardware;
            Device.Id = message.Device.Id;
            Device.Name = message.Device.Name;
            Device.Version = message.Device.Version;
        }


    }
}
