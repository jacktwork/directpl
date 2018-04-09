using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System;

namespace Services
{
  public class WeatherService : IWeatherService
  {
    public List<WeatherInfo> _list;
    public WeatherService()
    {
      FillWithDummyData();
    }
    public virtual async Task<IWeatherInfo> Get(ILocation location)
    {
      try
      {
        if (location is Location)
        {
          Location l = (Location)location;
          WeatherInfo w = _list.Where(a => a.Location.Country.ToLower() == l.Country.ToLower())
            .Where(b => b.Location.City.ToLower() == l.City.ToLower())
            .SingleOrDefault();
          if (w != null)
          {
            return await Task.FromResult(w);
          }
        }
        return await Task.FromResult(new WeatherInfo { Location = new Location { } });
      }
      catch (Exception ex)
      {
        Debug.WriteLine("ex: " + ex.Message);
      }
      return await Task.FromResult(new WeatherInfo { });
    }

    protected void FillWithDummyData()
    {
      _list = new List<WeatherInfo>();

      Location l = new Location { LocationId = 1, Country = "Poland", City = "Warsaw" };
      Temperature t = new Temperature { format = "Celcius", value = 16 };
      WeatherInfo w = new WeatherInfo { Location = l, Temperature = t, Humidity = 88 };
      _list.Add(w);

      l = new Location { LocationId = 1, Country = "Poland", City = "Gdansk" };
      t = new Temperature { format = "Celcius", value = 22 };
      w = new WeatherInfo { Location = l, Temperature = t, Humidity = 33 };
      _list.Add(w);

      l = new Location { LocationId = 1, Country = "Germany", City = "Berlin" };
      t = new Temperature { format = "Celcius", value = 44 };
      w = new WeatherInfo { Location = l, Temperature = t, Humidity = 55 };
      _list.Add(w);
    }
  }
}
