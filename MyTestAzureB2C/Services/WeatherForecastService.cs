using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using MyTestAzureB2C.Models;
using Newtonsoft.Json;

namespace MyTestAzureB2C.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly string _weatherForecastBaseAddress;
        private readonly string _weatherForecastScope;

        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;

        public WeatherForecastService(
            HttpClient httpClient,
            IConfiguration configuration,
            ITokenAcquisition tokenAcquisition)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _weatherForecastBaseAddress = configuration["WeatherForecast:BaseAddress"];
            _weatherForecastScope = configuration["WeatherForecast:Scope"];
        }


        public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{ _weatherForecastBaseAddress}/api/WeatherForecast");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);

                return res;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _weatherForecastScope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}