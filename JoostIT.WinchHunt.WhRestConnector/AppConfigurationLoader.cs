using IniFileParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace JoostIT.WinchHunt.WhRestConnector
{
    class AppConfigurationLoader
    {

        public const string helpString = "Usage: WinchHuntCmd [options]\n" +
                                        "   \n" +
                                        "   -f [config_file]:   The configuration file that contains the application configuration. Configuration " +
                                        "                       parameters in this file will override command line parameters.\n" +
                                        "   -p [com_port]:      The COM port name that the Winch Hunt serial device is connected to (as a full, " +
                                        "                       case-sensitive string)\n" +
                                        "   -r [rest_url]:      Optional. The full URL of the REST service to connect to and post the WinchHunt " +
                                        "                       data to\n" +
                                        "   -t [api_token]:     Optional and only if rest_url is specified. The access token to be able to post " +
                                        "                       to the REST service";



        public AppConfiguration LoadConfiguration(string[] args)
        {
            AppConfiguration config = ParseArgs(args);

            if (!String.IsNullOrWhiteSpace(config.ConfigurationFile))
            {
                LoadConfigFile(config);
            }

            return config;
        }


        private void LoadConfigFile(AppConfiguration config)
        {
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(config.ConfigurationFile);

            var comData = data.Global.GetKeyData("com_port");
            var apiTokenData = data.Global.GetKeyData("api_token");
            var restUrlData = data.Global.GetKeyData("rest_url");

            config.ComPort = comData != null ? comData.Value : config.ComPort;
            config.ApiAccessToken = apiTokenData != null ? apiTokenData.Value : config.ApiAccessToken;
            config.RestUrl = restUrlData != null ? restUrlData.Value : config.RestUrl;
        }


        private AppConfiguration ParseArgs(string[] args)
        {
            if (args.Length < 2) { ThrowInvalidArgumentsException(); }
            if (args.Length % 2 != 0) { ThrowInvalidArgumentsException(); }

            AppConfiguration config = new AppConfiguration();

            for (int i = 0; i < args.Length; i+=2)
            {
                string option = args[i];
                string value = args[i + 1];


                switch (option.ToLower())
                {
                    case "-p":
                        config.ComPort = value;
                        break;

                    case "-t":
                        config.ApiAccessToken = value;
                        break;

                    case "-r":
                        config.RestUrl = value;
                        break;

                    case "-f":
                        config.ConfigurationFile = value;
                        break;

                    default:
                        ThrowInvalidArgumentsException();
                        break;
                }               
            }

            return config;
        }


        private void ThrowInvalidArgumentsException()
        {
            throw new InvalidDataException("Invalid command line arguments.\n\n" + helpString);
        }

    }
}
