﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class NodeTemperatureService : ITemperatureService
    {
        public Task<TemperatureModel> GetTempAsync()
        {
            throw new NotImplementedException();
        }
    }
}
