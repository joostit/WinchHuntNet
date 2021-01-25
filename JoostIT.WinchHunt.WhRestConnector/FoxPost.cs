using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class FoxPost
    {

        /// <summary>
        /// Gets or sets the API Access token
        /// </summary>
        public string AccessToken { get; set; } = "";

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
