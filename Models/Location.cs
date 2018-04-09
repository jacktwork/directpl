
using Newtonsoft.Json;

namespace Models
{
  public class Location : ILocation
  {
    [JsonIgnore]
    public int LocationId
    {
      get;
      set;
    }
    public string City
    {
      get;
      set;
    }
    public string Country
    {
      get;
      set;
    }

  }
}
