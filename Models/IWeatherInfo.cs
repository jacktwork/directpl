using System;

namespace Models
{
  public interface IWeatherInfo
  {
    Temperature Temperature
    {
      get;
      set;
    }

    int Humidity
    {
      get;
      set;
    }
  }
}
