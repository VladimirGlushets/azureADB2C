using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTestAzureB2C.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyTestAzureB2C.Services;

namespace MyTestAzureB2C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public HomeController(
            ILogger<HomeController> logger, 
            IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> WeatherForecast()
        {
            var forcasts = await _weatherForecastService.GetWeatherForecastAsync();

            return View(forcasts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
