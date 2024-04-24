using _101InMemoryCacheInAspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace _101InMemoryCacheInAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ProductsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (_memoryCache.TryGetValue($"Product_{id}", out Product cachedProduct))
            {
                return Ok(cachedProduct);
            }
            else
            {
                var product = new Product { Id = id, Name = $"Product {id}", Price = 99.99M };

                _memoryCache.Set($"Product_{id}", product, TimeSpan.FromMinutes(10));

                return Ok(product);
            }
        }
    }
}