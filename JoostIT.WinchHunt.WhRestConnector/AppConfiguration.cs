using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JoostIT.WinchHunt.WhRestConnector
{
    public class AppConfiguration
    {

        public const string helpString = "Usage: WinchHuntCmd com_port [rest_url]\n" +
                                         "\n" +
                                         "com_port:    The COM port name that the Winch Hunt serial device is connected to (as a full, case-sensitive string)\n" +
                                         "[rest_url]:  Optional. The full URL of the REST service to connect to and post the WinchHunt data to";


        public bool ConnectToRest
        {
            get
            {
                return !String.IsNullOrEmpty(RestUrl);
            }
        }


        public string ComPort { get; set; }


        public string RestUrl { get; set; }


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
            if(args.Length < 1 || args.Length > 2)
            {
                ThrowInvalidArgumentsException();
            }


            ComPort = args[0];

            if(args.Length == 2)
            {
                RestUrl = args[1];
            }

        }


        private void ThrowInvalidArgumentsException()
        {
            throw new InvalidDataException("Invalid command line arguments.\n\n" + helpString);
        }
    }
}
