using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace OpenIdPoc.ApiConsoleClient
{
    class Program
    {
        // Console app Application ID  : 4a9fdf43-92e7-4ebf-bf77-c61ac69f7810
        // Console app Application Key : 9I56UEH+QliFrSYNX1/2K6ek24GFjATO32+VquUB8Xc=

        private const string AadInstance = "https://login.windows.net";
        private const string TenantName = "addition.dk";
        private const string ApplicationId = "4a9fdf43-92e7-4ebf-bf77-c61ac69f7810";
        private const string ApplicationKey = "9I56UEH+QliFrSYNX1/2K6ek24GFjATO32+VquUB8Xc=";

        private static readonly string Authority = $"{AadInstance}/{TenantName}";

        private const string ApiResourceId = "2cb886e3-0931-4fcf-a271-8ca0e52417a0";
        private const string ApiBaseAddress = "http://openidpoc.local/";

        private static HttpClient _httpClient = new HttpClient();
        private static AuthenticationContext _authContext = null;
        private static ClientCredential _clientCredential = null;



        static void Main(string[] args)
        {
            // setup auth
            _authContext = new AuthenticationContext(Authority);
            _clientCredential = new ClientCredential(ApplicationId, ApplicationKey);

            // call api
            GetDictionary().Wait();
            Console.ReadLine();
        }

        private static async Task GetDictionary()
        {
            // Authenticate and abort if authentication fails
            var result = await GetAuthResult<AuthenticationResult>();
            if (result == null)
            {
                Console.WriteLine("Cancelling service call");
                return;
            }

            // Setup request
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiBaseAddress}/api/dictionary");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to get dictionary");
                return;
            }

            string s = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Dictionary:\n{s}");
        }

        private static async Task<AuthenticationResult> GetAuthResult<T>()
        {
            var retry = false;
            var retryCount = 0;
            AuthenticationResult result = null;

            do
            {
                retry = false;
                try
                {
                    result = await _authContext.AcquireTokenAsync(ApiResourceId, _clientCredential);
                }
                catch (AdalException ex)
                {
                    if (ex.ErrorCode == "temporarily_unavailable")
                    {
                        retry = true;
                        retryCount++;
                        Thread.Sleep(3000);
                    }

                    Console.WriteLine($"Error occurred:\nEx   : {ex}\nRetry: {retryCount}");
                }
            } while (retry && (retryCount < 3));

            return result;
        }
    }
}
