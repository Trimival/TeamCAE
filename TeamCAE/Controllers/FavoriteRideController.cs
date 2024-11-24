using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCAE.Tables;

namespace TeamCAE.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteRideController : ControllerBase {
        private readonly TeamCAEDbContext _context;

        public FavoriteRideController(TeamCAEDbContext context) {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateFavoriteRide(FavoriteRide ride) {
            _context.FavoriteRides.Add(ride);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavoriteRide), new { id = ride.ID }, ride);
        }

        //Read - Single
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteRide>> GetFavoriteRide(int id) {
            var ride = await _context.FavoriteRides.FindAsync(id);
            if (ride == null) return NotFound();
            return ride;
        }

        //Read - Multiple
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteRide>>> GetFavoriteRides(int? id) {
            if (id == null || id == 0)
                return await _context.FavoriteRides.Take(5).ToListAsync();

            var ride = await _context.FavoriteRides.FindAsync(id);
            return ride == null ? NotFound() : new List<FavoriteRide> { ride };
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavoriteRide(int id, FavoriteRide ride) {
            if (id != ride.ID) return BadRequest();
            _context.Entry(ride).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteRide(int id) {
            var ride = await _context.FavoriteRides.FindAsync(id);
            if (ride == null) return NotFound();
            _context.FavoriteRides.Remove(ride);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}