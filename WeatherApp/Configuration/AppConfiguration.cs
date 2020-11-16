﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Configuration
{
    public static class AppConfiguration
    {
        private static IConfiguration configuration;

        public static string GetValue(string key)
        {
            if (configuration is null)
            {
                initConfig();
            }

            return configuration[key];
        }

        private static void initConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddUserSecrets<MainWindow>();

            configuration = builder.Build();
        }
    }
}
