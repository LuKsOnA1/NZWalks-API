using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RegionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        
        public IActionResult GetAllRegions()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://a.cdn-hotels.com/gdcs/production169/d1777/f6e2ce38-5276-4429-a4e1-a79947606630.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://tse1.mm.bing.net/th?id=OIP.bdbJrzwNlPq_QbPBLjhcWgHaEo&pid=Api&P=0&h=220"
                }
            };
            return Ok(regions);
        }
    }
}
