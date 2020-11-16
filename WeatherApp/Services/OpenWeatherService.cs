using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class OpenWeatherService : ITemperatureService
    {
        private static OpenWeatherProcessor owp;

        public TemperatureModel LastTemp { get; private set; }

        public OpenWeatherService(string apiKey)
        {
            owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = apiKey;
        }

        public async Task<TemperatureModel> GetTempAsync()
        {
            var cw = await owp.GetCurrentWeatherAsync();
            var temp = new TemperatureModel();

            temp.Temperature = cw.Main.Temperature;

            temp.DateTime = DateTime.UnixEpoch;
            temp.DateTime.AddSeconds(cw.DateTime);
            temp.DateTime = temp.DateTime.ToLocalTime();

            LastTemp = temp;
            return temp;
        }
    }
}
