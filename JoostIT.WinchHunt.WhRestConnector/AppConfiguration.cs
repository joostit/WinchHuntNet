using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class AppConfiguration
    {

        public const string helpString = "Usage: WinchHuntCmd com_port [rest_url] [api_access_token]\n" +
                                         "\n" +
                                         "com_port:    The COM port name that the Winch Hunt serial device is connected to (as a full, case-sensitive string)\n" +
                                         "[rest_url]:  Optional. The full URL of the REST service to connect to and post the WinchHunt data to\n" +
                                         "[api_access_token]:  Optional and only if rest_url is specified. The access token to be able to post to the REST service";


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
        /// Creates a new RunConfiguration based on command line args
        /// </summary>
        /// <param name="args"></param>
        public AppConfiguration(string[] args)
        {
            ParseArgs(args);
        }


        private void ParseArgs(string[] args)
        {
            if(args.Length < 1 || args.Length > 3)
            {
                ThrowInvalidArgumentsException();
            }


            ComPort = args[0];

            if(args.Length >= 2)
            {
                RestUrl = args[1];
            }

            if (args.Length == 3)
            {
                ApiAccessToken = args[2];
            }

        }


        private void ThrowInvalidArgumentsException()
        {
            throw new InvalidDataException("Invalid command line arguments.\n\n" + helpString);
        }
    }
}
