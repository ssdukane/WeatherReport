using Newtonsoft.Json;
using PRUD.Weather.Data;
using PRUD.Weather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PRUD.Weather.API.Tests
{
    public class UnitTest
    {
        private ICityWeather _cityweather;
        private IWebClient _client;

        public UnitTest(ICityWeather cityweather, IWebClient client)
        {
            _cityweather = cityweather;
            _client = client;
        }
        [Fact]
        public void TestGetWithCity()
        {
            string city = "london";

            var respose = _cityweather.GenerateReportForCity(city);

            dynamic test = JsonConvert.DeserializeObject<Report>(respose);

            if (test.Name.Equals("london_" + DateTime.Now.Date.ToShortDateString()))
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void TestGetWithCities()
        {
            string city = "london,pune";

            var respose = _cityweather.GenerateReportCitiwise(city.Split(','));

            dynamic test = JsonConvert.DeserializeObject<dynamic>(respose);

            foreach (var item in test)
            {
                if (item.Name.Equals(item.Name + "_" + DateTime.Now.Date.ToShortDateString()))
                {
                    Assert.True(true);
                }
                else
                {
                    Assert.False(false);
                }
            }
        }

        [Fact]
        public void TestGetWithUploadFile()
        {
            var fileName = "cities.txt";
            var folderName = Path.Combine("Reports");
            var pathToSave = Path.Combine(@"C:\Users\shaiduka\Desktop\CITEC\Repos\WeatherReport\PRUD.Weather.API\", folderName);
            var fullPath = Path.Combine(pathToSave, fileName);

            var cities = Utilities.ReadATextFile(fullPath);

            var respose = _cityweather.GenerateReportCitiwise(cities);

            dynamic test = JsonConvert.DeserializeObject<dynamic>(respose);

            foreach (var item in test)
            {
                if (item.Name.Equals(item.Name + "_" + DateTime.Now.Date.ToShortDateString()))
                {
                    Assert.True(true);
                }
                else
                {
                    Assert.False(false);
                }
            }
        }

        [Fact]
        public void TestWebClientResponse()
        {
            string city = "london";

            var respose = _client.Execute(city);

            var test = JsonConvert.DeserializeObject<WeatherReportForCity>(respose.Content);

            if (test.id == 294421)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(false);
            }
        }

        //[Fact]
        //public void WeatherControllerGet.CallsUploadWithCities()
        //{

        //}


    }
}
