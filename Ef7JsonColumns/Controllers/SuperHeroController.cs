using Ef7JsonColumns.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ef7JsonColumns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHeroes(List<SuperHero> heroes)
        {
            _context.Heroes.AddRange(heroes);

            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<List<SuperHero>>> GetHeroes(string city)
        {
            var heroes = await _context.Heroes
                .Include(h => h.Details)
                .Where(h => h.Details!.City
                .Contains(city)).ToListAsync();

            return Ok(heroes);
        }
    }
}
