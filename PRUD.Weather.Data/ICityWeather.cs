using PRUD.Weather.Models;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PRUD.Weather.Data
{
    interface ICityWeather
    {
        WeatherReportForCity GetReportForCity(string city);
        string GetReportForCityAsJson(string city);
    }

    public class CityWeather: ICityWeather
    {
        private RestClient Client { get; set; }

        public CityWeather()
        {            //api.openweathermap.org / data / 2.5 / weather ? q = London & appid = aa69195559bd4f88d79f9aadeb77a8f6
            Client = new RestClient("http://api.openweathermap.org/data/2.5/weather");
        }

        public WeatherReportForCity GetReportForCity(string city)
        {            
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "content-type");
            request.AddHeader("appid", "aa69195559bd4f88d79f9aadeb77a8f6");
            request.AddHeader("q", city);

            IRestResponse<WeatherReportForCity> response = Client.Execute<WeatherReportForCity>(request);

            return response.Data;
        }

        public string GetReportForCityAsJson(string city)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "content-type");
            request.AddParameter("appid", "aa69195559bd4f88d79f9aadeb77a8f6");
            request.AddParameter("q", city);

            IRestResponse<WeatherReportForCity> response = Client.Execute<WeatherReportForCity>(request);

            return response.Content;
        }
    }
}
