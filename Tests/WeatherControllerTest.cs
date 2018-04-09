using Models;
using NSubstitute;
using Services;
using Weather.Controllers;
using Xunit;

namespace Tests
{
  public class WeatherControllerTest
  {

    [Fact]
    public void Test1()
    {
      // warsaw location
      WeatherService weatherService = new WeatherService();
      var controller = new WeatherController(weatherService);
      var result = controller.Get("Poland", "Warsaw");
      WeatherInfo w = (WeatherInfo)result.Value;
      Assert.Equal("poland", w.Location.Country.ToLower());
      Assert.Equal("warsaw", w.Location.City.ToLower());
      Assert.Equal("Celcius", w.Temperature.format.ToString());
      Assert.Equal("16", w.Temperature.value.ToString());
      Assert.Equal("88", w.Humidity.ToString());
    }

    [Fact]
    public void Test2()
    {
      // gdansk location
      WeatherService weatherService = new WeatherService();
      var controller = new WeatherController(weatherService);
      var result = controller.Get("Poland", "Gdansk");
      WeatherInfo w = (WeatherInfo)result.Value;
      Assert.Equal("poland", w.Location.Country.ToLower());
      Assert.Equal("gdansk", w.Location.City.ToLower());
      Assert.Equal("Celcius", w.Temperature.format.ToString());
      Assert.Equal("22", w.Temperature.value.ToString());
      Assert.Equal("33", w.Humidity.ToString());
    }

    [Fact]
    public void Test3()
    {
      // not found location
      WeatherService weatherService = new WeatherService();
      var controller = new WeatherController(weatherService);
      var result = controller.Get("bogus", "fake");
      Assert.Equal("404", result.StatusCode.ToString());
    }

    [Fact]
    public void Test4()
    {
      // use NSubstitute to return a found location
      var ws = NSubstitute.Substitute.For<WeatherService>();
      WeatherInfo w = new WeatherInfo { Location = new Location { LocationId = 123, Country = "bogus", City = "fake" } };
      ws.Get(Arg.Any<Location>()).Returns(w);

      WeatherService weatherService = new WeatherService();
      var controller = new WeatherController(ws);
      var result = controller.Get("Poland", "Warsaw");
      WeatherInfo w2 = (WeatherInfo)result.Value;
      Assert.Equal("123", w2.Location.LocationId.ToString());
      Assert.Equal("bogus", w2.Location.Country.ToLower());
      Assert.Equal("fake", w2.Location.City.ToLower());
    }

    [Fact]
    public void Test5()
    {
      // use NSubstitute to return a not found location
      var ws = Substitute.For<WeatherService>();
      WeatherInfo w = new WeatherInfo { Location = new Location { LocationId = 0, Country = "bogus", City = "fake" } };
      ws.Get(Arg.Any<Location>()).Returns(w);

      WeatherService weatherService = new WeatherService();
      var controller = new WeatherController(ws);
      var result = controller.Get("Poland", "Warsaw");
      Assert.Equal("404", result.StatusCode.ToString());
    }


  }
}
