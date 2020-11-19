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
        public double Latitude { get; set; }

        /// <summary>
        /// Gets the longitute
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets the number of satellites currently being tracked
        /// </summary>
        public int Satellites { get; set; }

        /// <summary>
        /// Gets the speed (in km/h)
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets the altitude (in meters)
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Gets the GPS HDOP value
        /// </summary>
        public double Hdop { get; set; }

        /// <summary>
        /// Gets whether the GPS receiver has a valid fix
        /// </summary>
        public bool HasFix { get; set; }

    }
}
