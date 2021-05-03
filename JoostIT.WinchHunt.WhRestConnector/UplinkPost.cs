using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class UplinkPost
    {

        /// <summary>
        /// Gets or sets the list of Foxes
        /// </summary>
        public List<WinchFox> Devices { get; set; } = new List<WinchFox>();

        /// <summary>
        /// Gets or sets the Hunter that receives the LoRa signals
        /// </summary>
        public WinchHunter Hunter { get; set; }

    }
}
