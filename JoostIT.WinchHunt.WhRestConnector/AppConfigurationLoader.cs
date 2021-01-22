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
                                        "                       to the REST service" +
                                        "   -d                  Optional. Enabled Debug mode which will insert fake foxes into the data set.";



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
            var debugModeData = data.Global.GetKeyData("debug_mode");

            config.ComPort = comData != null ? comData.Value : config.ComPort;
            config.ApiAccessToken = apiTokenData != null ? apiTokenData.Value : config.ApiAccessToken;
            config.RestUrl = restUrlData != null ? restUrlData.Value : config.RestUrl;
            config.DebugMode = debugModeData != null ? bool.Parse(restUrlData.Value) : config.DebugMode;
        }


        private AppConfiguration ParseArgs(string[] args)
        {
            if (args.Length < 2) { ThrowInvalidArgumentsException(); }

            AppConfiguration config = new AppConfiguration();

            for (int i = 0; i < args.Length; i++)
            {
                string option = args[i];


                switch (option.ToLower())
                {
                    case "-p":
                        config.ComPort = args[++i];
                        break;

                    case "-t":
                        config.ApiAccessToken = args[++i];
                        break;

                    case "-r":
                        config.RestUrl = args[++i];
                        break;

                    case "-f":
                        config.ConfigurationFile = args[++i];
                        break;

                    case "-d":
                        config.DebugMode = true;
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
