using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface ITemperatureService
    {
        Task<TemperatureModel> GetTempAsync();
    }
}