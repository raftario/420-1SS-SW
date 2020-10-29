using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        public static double CelsisInFahrenheit(double c) => c * (9d / 5d) + 32;
    }
}
