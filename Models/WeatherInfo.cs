using System;

namespace Models
{
  public class WeatherInfo : IWeatherInfo
  {
    public Location Location
    {
      get;
      set;
    }

    public Temperature Temperature
    {
      get;
      set;
    }

    public int Humidity
    {
      get;
      set;
    }

  }
}
