using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Commands;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        public ITemperatureService TemperatureService;

        public DelegateCommand<string> GetTempCommand { get; set; }

        private TemperatureModel _currentTemp;
        public TemperatureModel CurrentTemp
        {
            get => _currentTemp;
            set
            {
                _currentTemp = value;
                OnPropertyChanged();
            }
        }

        public TemperatureViewModel()
        {
            GetTempCommand = new DelegateCommand<string>(GetTemp);
        }

        public void SetTemperatureService(ITemperatureService service)
        {
            TemperatureService = service;
        }

        public static double CelsisInFahrenheit(double c) => c * (9d / 5d) + 32;

        public static double FahrenheitInCelsius(double f) => (f - 32) * (5d / 9d);

        public bool CanGetTemp() => !(TemperatureService is null);

        private void GetTemp(object _)
        {
            if (TemperatureService is null)
            {
                throw new NullReferenceException("TemperatureService is null");
            }

            GetTempAsync();
        }
        private async void GetTempAsync()
        {
            CurrentTemp = await TemperatureService.GetTempAsync();
        }
    }
}
