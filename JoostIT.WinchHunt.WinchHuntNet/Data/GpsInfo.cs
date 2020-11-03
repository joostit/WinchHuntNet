using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.Data
{

    /// <summary>
    /// Holds GPS state information
    /// </summary>
    public class GpsInfo
    {
        /// <summary>
        /// Gets the latitude
        /// </summary>
        public double Latitude { get; internal set; }

        /// <summary>
        /// Gets the longitute
        /// </summary>
        public double Longitude { get; internal set; }

        /// <summary>
        /// Gets the number of satellites currently being tracked
        /// </summary>
        public int Satellites { get; internal set; }

        /// <summary>
        /// Gets the speed (in km/h)
        /// </summary>
        public double Speed { get; internal set; }

        /// <summary>
        /// Gets the altitude (in meters)
        /// </summary>
        public double Altitude { get; internal set; }

        /// <summary>
        /// Gets the GPS HDOP value
        /// </summary>
        public double Hdop { get; internal set; }

        /// <summary>
        /// Gets whether the GPS receiver has a valid fix
        /// </summary>
        public bool hasFix { get; internal set; }

    }
}
