﻿using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIt.WinchHunt.WhRestConnector
{
    class FakeDataGenerator
    {

        Random rand = new Random();

        public WinchHunter GetFakeHunter()
        {
            return new WinchHunter()
            {
                Device = new DeviceInfo()
                {
                    DeviceType = DeviceTypes.Hunter,
                    Hardware = "V1.2",
                    Id = "443322",
                    Name = "FakeHunt",
                    Version = 1
                },
                LastUpdate = DateTime.Now
            };
        }

        public void AddDebugFoxes(List<WinchFox> retVal)
        {
            // Launch site 1
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
                    LastUpdate = FakeAge(0),
                    LastRssi = FakeRssi(-45)
                });

            // Launch site 2
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
                    LastUpdate = FakeAge(0),
                    LastRssi = FakeRssi(-117)
                });

            // Near fictional winch
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

            // Lost one
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
                        Latitude = 52.275661,
                        Longitude = 6.894856
                    },
                    LastUpdate = FakeAge(5),
                    LastRssi = FakeRssi(-125)
                });


            retVal.Add(
                new WinchFox()
                {
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Hardware = "V1.2",
                        Id = "1212AA",
                        Name = "CalMarker",
                        Version = 1
                    },
                    Gps = new GpsInfo()
                    {
                        Altitude = FakeAltitude(35),
                        HasFix = true,
                        Hdop = FakeHdop(2.1),
                        Satellites = FakeSats(8),
                        Speed = FakeSpeed(0),
                        Latitude = 52.270090,
                        Longitude = 6.876613
                    },
                    LastUpdate = FakeAge(61),
                    LastRssi = FakeRssi(-125)
                });
        }


        private double FakeMotion(double initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + ((rand.NextDouble() - 0.5) * 0.00005);
            }
            else
            {
                return initial;
            }
        }


        private DateTime FakeAge(int minutes)
        {
            return DateTime.UtcNow - new TimeSpan(0, minutes, 0);
        }

        private int FakeRssi(int initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + rand.Next(-5, 5);
            }
            else
            {
                return initial;
            }
        }


        private int FakeAltitude(int initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + rand.Next(-6, 6);
            }
            else
            {
                return initial;
            }
        }


        private int FakeSats(int initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + rand.Next(-1, 1);
            }
            else
            {
                return initial;
            }
        }


        private double FakeSpeed(double initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + ((rand.NextDouble() - 0.5));
            }
            else
            {
                return initial;
            }
        }


        private double FakeHdop(double initial)
        {
            if (rand.Next(0, 3) < 1)
            {
                return initial + ((rand.NextDouble() - 0.5) * 0.1);
            }
            else
            {
                return initial;
            }
        }
    }
}
