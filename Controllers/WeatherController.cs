using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;
        private readonly string _weatherApiKey;

        public WeatherController(IConfiguration config, IDistributedCache cache)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _weatherApiKey = _config["WeatherApiKey"]
                ?? throw new ArgumentNullException("Weather API key is missing in User Secrets.");
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    return BadRequest(new { message = "City name cannot be empty." });
                }

                // Check Redis cache
                var cachedWeather = await _cache.GetStringAsync(city);
                if (!string.IsNullOrEmpty(cachedWeather))
                {
                    return Ok(JsonSerializer.Deserialize<object>(cachedWeather));
                }

                // Call external weather API
                var client = new RestClient("https://weather.visualcrossing.com");
                var request = new RestRequest($"/VisualCrossingWebServices/rest/services/timeline/{city}?key={_weatherApiKey}", Method.Get);

                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                {
                    return StatusCode((int)response.StatusCode, new { message = "Failed to fetch weather data.", error = response.ErrorMessage });
                }

                // Store in Redis cache for 12 hours
                await _cache.SetStringAsync(city, response.Content, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
                });

                return Ok(JsonSerializer.Deserialize<object>(response.Content));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
