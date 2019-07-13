using PRUD.Weather.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRUD.Weather.Data
{
    interface IWebClient
    {
        IRestResponse<WeatherReportForCity> Execute(string city);
    }

    public class WebClient : IWebClient
    {
        private RestClient Client { get; set; }
        private RestRequest Request { get; set; }

        public WebClient()
        {
            //api.openweathermap.org / data / 2.5 / weather ? q = London & appid = aa69195559bd4f88d79f9aadeb77a8f6
            Client = new RestClient("http://api.openweathermap.org/data/2.5/weather");
        }        

        /// <summary>
        /// Make request to main source to fetch detail for city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public IRestResponse<WeatherReportForCity> Execute(string city)
        {
            Request = new RestRequest(Method.GET);
            Request.AddHeader("content-type", "content-type");
            Request.AddHeader("appid", "aa69195559bd4f88d79f9aadeb77a8f6");
            Request.AddHeader("q", city);
            return Client.Execute<WeatherReportForCity>(Request);
        }
    }
}
