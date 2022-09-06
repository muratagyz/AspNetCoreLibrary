using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { Id = 1, Name = "Kalem", Price = 10 });
        }

        [HttpGet("{name}")]
        public IActionResult GetProduct(string name)
        {
            return Ok(new { Id = 2, Name = name, Price = 20 });
        }

        [HttpPut]
        public IActionResult UpdateProduct()
        {
            return Ok();
        }
    }
}
