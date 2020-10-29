using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        public static double CelsisInFahrenheit(double c) => c * (9d / 5d) + 32;

        public static double FahrenheitInCelsius(double f) => (f - 32) * (5d / 9d);
    }
}
