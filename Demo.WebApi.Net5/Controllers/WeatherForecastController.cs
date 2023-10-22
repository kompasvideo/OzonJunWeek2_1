using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public WeatherForecast Get3([FromQuery(Name="parameter")] string summary)
        {
            return new WeatherForecast
            {
                Summary = summary
            };
        }
        
        [HttpGet("summary")]
        public WeatherForecast Get4([FromRoute] string summary)
        {
            return new WeatherForecast
            {
                Summary = summary
            };
        }
        
        [HttpPost("post")]
        public WeatherForecast Post1([FromBody]ComplexType x)
        {
            return new WeatherForecast
            {
                Summary = $"{x.SingleType} {x.Id} {x.SingleType?.Value}"
            };
        }
        
        [HttpPost("post")]
        public IActionResult Post([FromBody]ComplexType x)
        {
            var context = new ValidationContext(x);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(x, context, validationResults, true);
            if (!isValid)
            {
                var errors = validationResults.Select(x => new Error()
                {
                    Message = $"{x.ErrorMessage}"
                }).ToArray();
                return BadRequest(errors);
            }
            return Ok(new WeatherForecast
            {
                Summary = $"{x.SingleType} {x.Id} {x.SingleType?.Value}"
            });
        }
    }

    public class Error
    {
        public string Message { get; set; }
    }

    public class ComplexType
    {
        public string StringValue { get; set; }
        public int Id { get; set; }
        public SingleType SingleType { get; set; }
    }

    public class SingleType
    {
        public string Value { get; set; }
    }
}