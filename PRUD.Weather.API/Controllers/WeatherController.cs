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


namespace PRUD.Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IConfiguration configuration;
        private CityWeather CityWeather;

        public WeatherController()
        {
            CityWeather = new CityWeather();
            
        }
        // GET api/weather
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var  CityWeather.GetReportForCity("london");
            return new string[] { "welcome to Weather Report Service", "-----@@@----" };
        }

        // GET api/weather/"london"
        [HttpGet("{city}")]
        public ActionResult<string> Get(string city)
        {
            var data = CityWeather.GenerateReportForCity(city);

            return data;
        }

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

                    var result = CityWeather.GenerateReportCitiwise(content);
                                                         
                    return Ok(new { dbPath , result });
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

        //[HttpPost, DisableRequestSizeLimit]
        //public IActionResult Upload(char separator)
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        var folderName = Path.Combine("Reports");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            if (separator == ' ')
        //            {
        //                separator = ',';                        
        //            }

        //            var content = Utilities.ReadATextFile(fullPath);

        //            var result = CityWeather.GenerateReportCitiwise(content);

        //            return Ok(new { dbPath, result });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        // PUT api/weather/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/weather/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
