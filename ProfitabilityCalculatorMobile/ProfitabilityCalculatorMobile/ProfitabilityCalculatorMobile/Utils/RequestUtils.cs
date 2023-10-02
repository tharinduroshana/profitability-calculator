using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfitabilityCalculatorMobile.Models;

namespace ProfitabilityCalculatorMobile.Utils
{
    public class RequestUtils
    {
        public static async Task<HttpResponseMessage> SendPostRequest(string url, object data)
        {
            var jsonString = JsonConvert.SerializeObject(data);

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);

                var jsonData = new StringContent (jsonString, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync(url, jsonData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}