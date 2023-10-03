using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

/*
 * The RequestUtils class contains SendPostRequest method which sends POST requests for the backend.
 */
namespace ProfitabilityCalculatorMobile.Utils
{
    public class RequestUtils
    {
        public static async Task<HttpResponseMessage> SendPostRequest(string url, object data, bool needsAuth = false)
        {
            var jsonString = JsonConvert.SerializeObject(data);

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);

                if (needsAuth)
                {
                    var accessToken = await SecureStorage.GetAsync("token");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                }

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