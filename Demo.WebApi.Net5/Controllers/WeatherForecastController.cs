using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.WebApi.Net5.Controllers
{
    [ApiController]
    // [Route("[controller]")]
    [Route("Weather/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        // [HttpGet("Get/1")] //  WeatherForecast/Get/1
        // [HttpGet("/Get/2")] //  /Get/2
        // [HttpGet("/[controller]/Get/3")] //  WeatherForecast/Get/3
        // [HttpGet("/WeatherForecast/Get/4")] //  WeatherForecast/Get/4
        // [HttpGet("/[controller]/[action]/5")] //  WeatherForecast/Get/5
        // [HttpGet("[controller]/[action]/6")] //  WeatherForecast/WeatherForecast/Get/6
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = "Summary"
                })
                .ToArray();
        }
        public class ErrorType
        {
            public string ErrorMessage { get; set; }
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(WeatherForecast), 200)]
        [ProducesResponseType(typeof(ErrorType), 400)]
        public IActionResult Get2()
        {
            return BadRequest(new ErrorType
            {
              ErrorMessage = "2"   
            });
        }
        
        [HttpGet]
        public WeatherForecast Get3()
        {
            return new WeatherForecast
            {
                
            };
        }
    }
}