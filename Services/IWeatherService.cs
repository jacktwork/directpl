using Models;
using System.Threading.Tasks;

namespace Services
{
  public interface IWeatherService
  {
    Task<IWeatherInfo> Get(ILocation location);
  }
}
