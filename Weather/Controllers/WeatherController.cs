using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;
using Services;
using System;
using System.Diagnostics;

namespace Weather.Controllers
{
  public class WeatherController : Controller
  {
    private IWeatherService _weatherService;
    public WeatherController(WeatherService weatherService)
    {
      _weatherService = weatherService;
    }

    [HttpGet]
    [HttpGet("AsJson")]
    [Produces("application/json")]
    [Route("api/weather/{country:alpha}/{city:alpha}")]
    public ObjectResult Get(string country, string city)
    {
      try
      {
        ILocation location = new Location { Country = country, City = city };

        var result = _weatherService.Get(location);
        if (result.Result is WeatherInfo)
        {
          WeatherInfo weatherInfo = (WeatherInfo)result.Result;
          if (weatherInfo.Location.LocationId == 0)
          {
            string str = $"Cannot find country: {country} city: {city}";
            return StatusCode(404, new ErrorMessage { error = str });
          }
          return StatusCode(200, weatherInfo);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("ex: " + ex.Message);
      }
      return StatusCode(500, new ErrorMessage { error = "Server error.  Please try again later." });
    }
  }
}
