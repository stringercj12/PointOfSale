using Microsoft.AspNetCore.Mvc;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index => Summaries[index]).ToArray();
        }
    }
}
