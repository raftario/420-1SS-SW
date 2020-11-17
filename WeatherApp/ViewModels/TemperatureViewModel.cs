using System;
using System.Threading.Tasks;
using WeatherApp.Commands;
using WeatherApp.Configuration;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        private TemperatureModel currentTemp;

        public ITemperatureService TemperatureService { get; private set; }

        public DelegateCommand<string> GetTempCommand { get; set; }

        public TemperatureModel CurrentTemp 
        { 
            get => currentTemp;
            set
            {
                currentTemp = value;
                OnPropertyChanged();
                OnPropertyChanged("RawText");
            }
        }

        public string RawText {
            get {
                if (CurrentTemp != null)
                    return $"Time : {CurrentTemp.DateTime.ToLocalTime()} {Environment.NewLine}Temperature : {CurrentTemp.Temperature}";
                else
                    return string.Empty;
            }
        }

        public TemperatureViewModel()
        {
            GetTempCommand = new DelegateCommand<string>(GetTemp, CanGetTemp);
            TemperatureService = new OpenWeatherService(AppConfiguration.GetValue("api-key"));
            OpenWeatherAPI.ApiHelper.InitializeClient();
        }

        public bool CanGetTemp(string obj)
        {
            return TemperatureService != null;
        }

        public void GetTemp(string obj)
        {
            if (TemperatureService == null) throw new NullReferenceException();

            _ = GetTempAsync();
        }

        private async Task GetTempAsync()
        {
            CurrentTemp = await TemperatureService.GetTempAsync();
        }

        public void SetTemperatureService(ITemperatureService srv)
        {
            TemperatureService = srv;
        }
    }
}
