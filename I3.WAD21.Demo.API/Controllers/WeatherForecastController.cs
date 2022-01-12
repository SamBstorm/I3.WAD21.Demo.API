using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace I3.WAD21.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public static List<WeatherForecast> forecasts = new List<WeatherForecast>();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Doit être supérieur à 0");
            var rng = new Random();
            return Enumerable.Range(1, id).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(nameof(GetOne))]
        public WeatherForecast GetOne()
        {
            return new WeatherForecast { Date = DateTime.Now, TemperatureC = 0, Summary = "Toto" };
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast forecast)
        {
            if (forecast is null) return BadRequest();
            forecasts.Add(forecast);
            return Ok();
        }
    }
}
