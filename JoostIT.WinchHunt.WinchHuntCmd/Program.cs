using JoostIT.WinchHunt.WinchHuntNet;
using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace JoostIT.WinchHunt.WinchHuntCmd
{
    class Program
    {
        static void Main(string[] args)
        {

        }


        [Conditional("DEBUG")]
        private static void AddDebugFoxes(List<WinchFox> retVal)
        {

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "112233",
                        Name = "FakeFox1",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.278721,
                        Longitude = 6.898578
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "AABBCC",
                        Name = "FakeFox2",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.278655,
                        Longitude = 6.898686
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "55EE66",
                        Name = "FakeFox3",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.271880,
                        Longitude = 6.882657
                    },
                    LastUpdate = DateTime.UtcNow
                });

            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "77FF88",
                        Name = "FakeFox4",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 2.1,
                        Satellites = 6,
                        Speed = 0.01,
                        Latitude = 52.275661,
                        Longitude = 6.894856
                    },
                    LastUpdate = DateTime.UtcNow
                });
        }


    }
}
