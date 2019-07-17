using PRUD.Weather.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace PRUD.Weather.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICityWeather
    {
        WeatherReportForCity GetReportForCity(string city);
        string GetReportForCityAsJson(string city);

        string GenerateReportForCity(string city);
        string GenerateReportCitiwise(IEnumerable<string> cities);
    }
        
    public class CityWeather : ICityWeather
    {
        private IWebClient _client { get; set; }

        public CityWeather(IWebClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get weather details in model form for single city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public WeatherReportForCity GetReportForCity(string city)
        {
            var response = _client.Execute(city);

            return response.Data;
        }

        /// <summary>
        /// Get weather details in json form for single city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string GetReportForCityAsJson(string city)
        {
            var response = _client.Execute(city);

            return response.Content;
        }

        /// <summary>
        /// Generate weather report for city and save in specified folder
        /// </summary>
        /// <param name="city"></param>
        /// <returns>Status of execution</returns>
        public string GenerateReportForCity(string city)
        {
            try
            {
                var response = _client.Execute(city);

                var result = Utilities.SaveWeatherReportForCity(city, response.Content);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Generate weather report for cities and save in specified folder
        /// </summary>
        /// <param name="cities"></param>
        /// <returns>Status of execution</returns>
        public string GenerateReportCitiwise(IEnumerable<string> cities)
        {
            try
            {
                dynamic result = new ExpandoObject();
                foreach (var city in cities)
                {
                    var response = _client.Execute(city);
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
