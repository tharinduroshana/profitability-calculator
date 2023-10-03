/*
 * Contains Base Url for the API. This should be changed in the deployment with the diployed url.
 */
namespace ProfitabilityCalculatorMobile
{
    public static class Constants
    {
#if DEBUG
        public const string ApiBaseUrl = "http://10.0.2.2:5105";
#else
        public const string ApiBaseUrl = "https://api.example.com/";
#endif
    }
}