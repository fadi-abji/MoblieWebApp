using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly ILogger<RedisController> _logger;
        private readonly IConnectionMultiplexer _redis;

        public RedisController(ILogger<RedisController> logger, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
        }

        // GET method to retrieve weather data from Redis
        [HttpGet]
        public IActionResult Get()
        {
            var db = _redis.GetDatabase();

            // Try to get weather data from Redis
            var weatherDataJson = db.StringGet("weatherData");
            if (!weatherDataJson.HasValue)
            {
                return NotFound("Weather data not found.");
            }

            // Deserialize the data from Redis back into WeatherForecast objects
            var weatherData = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(weatherDataJson);
            return Ok(weatherData);
        }

        // POST method to accept and store weather data in Redis
        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<WeatherForecast> weatherForecasts)
        {
            var db = _redis.GetDatabase();

            // Serialize the weather data to JSON
            var weatherDataJson = JsonSerializer.Serialize(weatherForecasts);

            // Store the serialized data in Redis with a key
            db.StringSet("weatherData", weatherDataJson);

            return Ok("Weather data has been stored in Redis.");
        }
    }
}
