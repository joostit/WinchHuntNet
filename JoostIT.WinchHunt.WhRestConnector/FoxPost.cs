using JoostIT.WinchHunt.WinchHuntNet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class FoxPost
    {

        public string AccessToken { get; set; } = "";

        public List<WinchFox> Devices { get; set; } = new List<WinchFox>();

    }
}
