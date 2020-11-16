using OpenWeatherAPI;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OpenWeatherAPITests
{
    public class OpenWeatherProcessorTests
    {
        [Fact]
        public async Task GetOneCallAsync_IfApiKeyEmptyOrNull_ThrowArgumentException()
        {
            var owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = null;

            async Task testCode() { await owp.GetOneCallAsync(); }

            var ex = await Assert.ThrowsAsync<ArgumentException>(testCode);
            Assert.Contains("key", ex.Message);
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_IfApiKeyEmptyOrNull_ThrowArgumentException()
        {
            var owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = null;

            async Task testCode() { await owp.GetCurrentWeatherAsync(); }

            var ex = await Assert.ThrowsAsync<ArgumentException>(testCode);
            Assert.Contains("key", ex.Message);
        }

        [Fact]
        public async Task GetOneCallAsync_IfApiHelperNotInitialized_ThrowArgumentException()
        {
            var owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = "key";
            ApiHelper.ApiClient = null;

            async Task testCode() { await owp.GetOneCallAsync(); }

            var ex = await Assert.ThrowsAsync<ArgumentException>(testCode);
            Assert.Contains("HTTP", ex.Message);
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_IfApiHelperNotInitialized_ThrowArgumentException()
        {
            var owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = "key";
            ApiHelper.ApiClient = null;

            async Task testCode() { await owp.GetCurrentWeatherAsync(); }

            var ex = await Assert.ThrowsAsync<ArgumentException>(testCode);
            Assert.Contains("HTTP", ex.Message);
        }
    }
}
