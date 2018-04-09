using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> DisplayResult()
    {
      string country = Request.Form["country"].ToString();
      string city = Request.Form["city"].ToString();

      using (var client = new HttpClient())
      {
        var request = HttpContext.Request;
        string host = request.Host.Host;
        int? port = request.Host.Port;
        string portstr = String.Empty;
        if (port != null)
        {
          portstr = ":" + port;
        }

        string url = $"http://{host}{portstr}/api/weather/{country}/{city}"; 
        var uri = new Uri(url);

        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
          string json = await response.Content.ReadAsStringAsync();
          WeatherInfo w = JsonConvert.DeserializeObject<WeatherInfo>(json);
          return View(w); //Ok(w);
        }
      }

      return BadRequest();
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
