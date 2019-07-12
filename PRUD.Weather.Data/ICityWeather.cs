using PRUD.Weather.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace PRUD.Weather.Data
{
    interface ICityWeather
    {
        WeatherReportForCity GetReportForCity(string city);
        string GetReportForCityAsJson(string city);
    }

    public class CityWeather : ICityWeather
    {
        private WebClient Client { get; set; }

        public CityWeather()
        {
            Client = new WebClient();
        }

        public WeatherReportForCity GetReportForCity(string city)
        {
            var response = Client.Execute(city);

            return response.Data;
        }

        public string GetReportForCityAsJson(string city)
        {
            var response = Client.Execute(city);

            return response.Content;
        }

        public string GenerateReportForCity(string city)
        {
            try
            {
                var response = Client.Execute(city);

                var result = Utilities.SaveWeatherReportForCity(city, response.Content);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string GenerateReportCitiwise(IEnumerable<string> cities)
        {
            try
            {
                dynamic result = new ExpandoObject();
                foreach (var city in cities)
                {
                    var response = Client.Execute(city);
                    Utilities.AddProperty(result, city, Utilities.SaveWeatherReportForCity(city, response.Content));
                }

                return JsonConvert.SerializeObject(result); ;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
