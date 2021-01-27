using JoostIt.WinchHunt.WhRestConnector;
using JoostIT.WinchHunt.WinchHuntNet.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JoostIT.WinchHunt.WhRestConnector
{
    internal class WebRestClient
    {

        private readonly HttpClient client = new HttpClient();

        private AppConfiguration configuration;

        public WebRestClient(AppConfiguration config)
        {
            configuration = config;
        }


        public void sendFoxes(UplinkPost devices)
        {

            if (String.IsNullOrEmpty(configuration.RestUrl))
            {
                throw new InvalidOperationException("Cannot send foxes when no Rest URL is configured");
            }

            string content = JsonConvert.SerializeObject(devices);
            try
            {
                var httpResponse = client.PostAsync(configuration.RestUrl, new StringContent(content, Encoding.Default, "application/json")).Result;
                if (!httpResponse.IsSuccessStatusCode)
                {
                    Logger.Log("REST Uplink error: " + httpResponse.ToString());
                }
            }
            catch (AggregateException e)
            {
                Logger.Log("REST Uplink error: " + e.Flatten().Message);
            }

        }

    }
}
