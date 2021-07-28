using System.Collections.Generic;
using System.Threading.Tasks;
using MyTestAzureB2C.Models;

namespace MyTestAzureB2C.Services
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync();
    }
}
