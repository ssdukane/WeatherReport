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

    public class Webclient : IWebClient
    {
        private RestClient Client { get; set; }
        private RestRequest Request { get; set; }

        public Webclient()
        {
            //api.openweathermap.org / data / 2.5 / weather ? q = London & appid = aa69195559bd4f88d79f9aadeb77a8f6
            Client = new RestClient("http://api.openweathermap.org/data/2.5/weather");

            Request = new RestRequest(Method.GET);
            Request.AddHeader("content-type", "content-type");
            Request.AddHeader("appid", "aa69195559bd4f88d79f9aadeb77a8f6");
        }        

        public IRestResponse<WeatherReportForCity> Execute(string city)
        {
            Request.AddHeader("q", city);
            return Client.Execute<WeatherReportForCity>(Request);
        }
    }
}
