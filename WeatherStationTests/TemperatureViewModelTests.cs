﻿using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Xunit;
using Xunit.Sdk;

namespace WeatherStationTests
{
    public class TemperatureViewModelTests : IDisposable
    {
        // System Under Test
        // Utilisez ce membre dans les tests
        TemperatureViewModel _sut = new TemperatureViewModel();

        /// <summary>
        /// Test la fonctionnalité de conversion de Celsius à Fahrenheit
        /// </summary>
        /// <param name="C">Degré Celsius</param>
        /// <param name="expected">Résultat attendu</param>
        /// <remarks>T01</remarks>
        [Theory]
        [InlineData(0, 32)]
        [InlineData(-40, -40)]
        [InlineData(-20, -4)]
        [InlineData(-17.8, 0)]
        [InlineData(37, 98.6)]
        [InlineData(100, 212)]
        public void CelsiusInFahrenheit_AlwaysReturnGoodValue(double C, double expected)
        {
            // Arrange

            // Act       
            var actual = TemperatureViewModel.CelsisInFahrenheit(C);

            // Assert
            Assert.Equal(expected, actual, 1);
        }

        /// <summary>
        /// Test la fonctionnalité de conversion de Fahrenheit à Celsius
        /// </summary>
        /// <param name="F">Degré F</param>
        /// <param name="expected">Résultat attendu</param>
        /// <remarks>T02</remarks>
        [Theory]
        [InlineData(32, 0)]
        [InlineData(-40, -40)]
        [InlineData(-4, -20)]
        [InlineData(0, -17.8)]
        [InlineData(98.6, 37)]
        [InlineData(212, 100)]
        public void FahrenheitInCelsius_AlwaysReturnGoodValue(double F, double expected)
        {
            // Arrange

            // Act       
            var actual = TemperatureViewModel.FahrenheitInCelsius(F);

            // Assert
            Assert.Equal(expected, actual, 1);
        }

        /// <summary>
        /// Lorsqu'exécuté GetTempCommand devrait lancer une NullException
        /// </summary>
        /// <remarks>T03</remarks>
        [Fact]
        public void GetTempCommand_ExecuteIfNullService_ShouldThrowNullException()
        {
            // Arrange
            void testCode() => _sut.GetTempCommand.Execute(null);

            // Act       

            // Assert
            Assert.Throws<NullReferenceException>(testCode);
        }

        /// <summary>
        /// La méthode CanGetTemp devrait retourner faux si le service est null
        /// </summary>
        /// <remarks>T04</remarks>
        [Fact]
        public void CanGetTemp_WhenServiceIsNull_ReturnsFalse()
        {
            // Arrange
            _sut.SetTemperatureService(null);

            // Act       
            var condition = _sut.CanGetTemp();

            // Assert
            Assert.False(condition);
        }

        /// <summary>
        /// La méthode CanGetTemp devrait retourner vrai si le service est instancié
        /// </summary>
        /// <remarks>T05</remarks>
        [Fact]
        public void CanGetTemp_WhenServiceIsSet_ReturnsTrue()
        {
            // Arrange
            _sut.SetTemperatureService(new OpenWeatherService());

            // Act       
            var condition = _sut.CanGetTemp();

            // Assert
            Assert.True(condition);
        }

        /// <summary>
        /// TemperatureService ne devrait plus être null lorsque SetTemperatureService
        /// </summary>
        /// <remarks>T06</remarks>
        [Fact]
        public void SetTemperatureService_WhenExecuted_TemperatureServiceIsNotNull()
        {
            // Arrange

            // Act       
            _sut.SetTemperatureService(new OpenWeatherService());

            // Assert
            Assert.NotNull(_sut.TemperatureService);
        }

        /// <summary>
        /// CurrentTemp devrait avoir une valeur lorsque GetTempCommand est exécutée
        /// </summary>
        /// <remarks>T07</remarks>
        [Fact]
        public void GetTempCommand_HaveCurrentTempWhenExecuted_ShouldPass()
        {
            // Arrange
            var mock = new Mock<ITemperatureService>();
            mock.Setup((s) => s.GetTempAsync())
                .ReturnsAsync(new TemperatureModel { DateTime = DateTime.Now, Temperature = 2112 });
            _sut.SetTemperatureService(mock.Object);

            // Act       
            _sut.GetTempCommand.Execute(null);

            // Assert
            Assert.Equal(2112, _sut.CurrentTemp.Temperature);
        }

        /// <summary>
        /// Ne touchez pas à ça, c'est seulement pour respecter les standards
        /// de test qui veulent que la classe de tests implémente IDisposable
        /// Mais ça peut être utilisé, par exemple, si on a une connexion ouverte, il
        /// faut s'assurer qu'elle sera fermée lorsque l'objet sera détruit
        /// </summary>
        public void Dispose()
        {
            // Nothing to here, just for Testing standards
        }
    }
}
