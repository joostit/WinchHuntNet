using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIt.WinchHunt.WhRestConnector
{
    class FakeData
    {

        Random rand = new Random();

        public void AddDebugFoxes(List<WinchFox> retVal)
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
                        Altitude = FakeAltitude(36),
                        HasFix = true,
                        Hdop = FakeHdop(1.9),
                        Satellites = FakeSats(6),
                        Speed = FakeSpeed(0),
                        Latitude = FakeMotion(52.278721),
                        Longitude = FakeMotion(6.898578)
                    },
                    LastUpdate = DateTime.UtcNow,
                    LastRssi = FakeRssi(-45)
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
                        Altitude = FakeAltitude(34),
                        HasFix = true,
                        Hdop = FakeHdop(1.7),
                        Satellites = FakeSats(6),
                        Speed = FakeSpeed(0),
                        Latitude = FakeMotion(52.278655),
                        Longitude = FakeMotion(6.898686)
                    },
                    LastUpdate = DateTime.UtcNow,
                    LastRssi = FakeRssi(-117)
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
                        Altitude = FakeAltitude(32),
                        HasFix = true,
                        Hdop = FakeHdop(1.5),
                        Satellites = FakeSats(6),
                        Speed = FakeSpeed(0),
                        Latitude = FakeMotion(52.271880),
                        Longitude = FakeMotion(6.882657)
                    },
                    LastUpdate = DateTime.UtcNow,
                    LastRssi = FakeRssi(-64)
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
                        Altitude = FakeAltitude(35),
                        HasFix = true,
                        Hdop = FakeHdop(2.1),
                        Satellites = FakeSats(6),
                        Speed = FakeSpeed(0),
                        Latitude = FakeMotion(52.275661),
                        Longitude = FakeMotion(6.894856)
                    },
                    LastUpdate = DateTime.UtcNow,
                    LastRssi = FakeRssi(-125)
                });
        }


        private double FakeMotion(double initial)
        {
            return initial + ((rand.NextDouble() - 0.5) * 0.00005);
        }


        private int FakeRssi(int initial)
        {
            return initial + rand.Next(-5, 5);
        }


        private int FakeAltitude(int initial)
        {
            return initial + rand.Next(-6, 6);
        }


        private int FakeSats(int initial)
        {
            return initial + rand.Next(-1, 1);
        }


        private double FakeSpeed(double initial)
        {
            return initial + ((rand.NextDouble() - 0.5));
        }


        private double FakeHdop(double initial)
        {
            return initial + ((rand.NextDouble() - 0.5) * 0.1);
        }
    }
}
