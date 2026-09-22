using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ECommerce.API.Controllers
{
    [Route("[controller]")]
    public class DistiributedCacheController : Controller
    {
        IDatabase _database;

        public DistiributedCacheController(IConnectionMultiplexer connectionMultiplexer) 
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        [HttpGet("get/{Key}")]
        public IActionResult Get(string Key) {

            var CacheValue = _database.StringGet(Key);
            
            if (string.IsNullOrEmpty(CacheValue)) 
                return NotFound();
            return Ok(CacheValue.ToString());

        }
    }
}