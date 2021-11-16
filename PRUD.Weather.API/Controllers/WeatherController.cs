using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using PRUD.Weather.Data;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PRUD.Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private ICityWeather _cityWeather;
        private static readonly Regex Validator = new Regex(@",.;'");

        public WeatherController(ICityWeather cityWeather)
        {
            _cityWeather = cityWeather;
        }

        /// <summary>
        /// Default message
        /// </summary>
        /// <returns></returns>
        // GET api/weather
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var  CityWeather.GetReportForCity("london");
            return new string[] { "welcome to Weather Report Service", "-----@@@----" };
        }

        /// <summary>
        /// Generate weather report for single city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        // GET api/weather/"london"
        [HttpGet("{city}")]
        public ActionResult<string> Get(string city)
        {
            try
            {
                //if (city.Any(ch => !Char.IsLetterOrDigit(ch)) || city.Any(ch => !Char.IsDigit(ch)))
                if (city.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    var error = "Incorrect city name. Input string without any special character!";
                    return Ok(new { city, error });
                }
                else
                {
                    var data = _cityWeather.GenerateReportForCity(city);
                    return data;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
          /// <summary>
        /// Generate weather report for single city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        // GET api/weather/"london"
        [HttpGet("{city}")]
        public ActionResult<string> Get(int cityID)
        {
            try
            {
                //if (city.Any(ch => !Char.IsLetterOrDigit(ch)) || city.Any(ch => !Char.IsDigit(ch)))
                
                var city = "Mumbai"   // DAL.GetCityByID(cityID);
                //Bug
                return city
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Generate weather report for cities by uploading list in .txt file
        /// </summary>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Reports");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var content = Utilities.ReadATextFile(fullPath);

                    var result = _cityWeather.GenerateReportCitiwise(content);

                    return Ok(new { dbPath, result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
