using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.Data
{
    public class GpsInfo
    {

        public double Latitude { get; internal set; }

        public double Longitude { get; internal set; }

        public int Satellites { get; internal set; }

        public double Speed { get; internal set; }

        public double Altitude { get; internal set; }

        public double Hdop { get; internal set; }

        public bool hasFix { get; internal set; }

    }
}
