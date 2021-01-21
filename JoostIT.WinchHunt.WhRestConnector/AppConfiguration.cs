using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class AppConfiguration
    {

       

        public bool ConnectToRest
        {
            get
            {
                return !String.IsNullOrEmpty(RestUrl);
            }
        }

        /// <summary>
        /// Gets the COM port
        /// </summary>
        public string ComPort { get; set; }

        /// <summary>
        /// Gets the rest URL
        /// </summary>
        public string RestUrl { get; set; }


        /// <summary>
        /// Gets the API access token
        /// </summary>
        public string ApiAccessToken { get; set; } = "";

        /// <summary>
        /// Gets or sets the configuration file
        /// </summary>
        public string ConfigurationFile { get; set; }


        /// <summary>
        /// Creates a new RunConfiguration based on command line args
        /// </summary>
        public AppConfiguration()
        {
            
        }


        
    }
}
