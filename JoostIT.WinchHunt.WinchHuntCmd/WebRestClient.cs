using JoostIT.WinchHunt.WinchHuntNet.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JoostIT.WinchHunt.WinchHuntCmd
{
    internal class WebRestClient
    {

        private const string BaseUrl = "https://winchhunt.azurewebsites.net/api/foxes";

        private static readonly HttpClient client = new HttpClient();


        public void sendFoxes(FoxPost devices)
        {

            string content = JsonConvert.SerializeObject(devices);
            try
            {
                var httpResponse = client.PostAsync(BaseUrl, new StringContent(content, Encoding.Default, "application/json")).Result;

            }
            catch (AggregateException e)
            {
                Console.WriteLine("Error while sending Fox Update to web server: " + e.Flatten().Message);
            }

            //var createdTask = JsonConvert.DeserializeObject<Todo>(await httpResponse.Content.ReadAsStringAsync());
            //return createdTask;
        }

    }
}
