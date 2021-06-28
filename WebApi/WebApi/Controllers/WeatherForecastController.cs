using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly AppDatabaseContext _appDatabaseContext;

        public WeatherForecastController(AppDatabaseContext appDatabaseContext)
        {
            _appDatabaseContext = appDatabaseContext;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpPost]
        public void Post([FromBody] string value)
        {
            User user = new User { Name = value, Age = 22 };
            _appDatabaseContext.Users.Add(user);
            _appDatabaseContext.SaveChanges();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _appDatabaseContext.Users.Find(id).Name;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var users = _appDatabaseContext.Users.Select(u => u.Name).ToArray();
            return users;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            User user = _appDatabaseContext.Users.Find(id);
            user.Name = value;
            _appDatabaseContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = _appDatabaseContext.Users.Find(id);
            _appDatabaseContext.Users.Remove(user);
            _appDatabaseContext.SaveChanges();
        }
    }
}
