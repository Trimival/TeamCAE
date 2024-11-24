using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCAE.Tables;

namespace TeamCAE.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteFoodController : ControllerBase {
        private readonly TeamCAEDbContext _context;

        public FavoriteFoodController(TeamCAEDbContext context) {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateFavoriteFood(FavoriteFood food) {
            _context.FavoriteFoods.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavoriteFood), new { id = food.ID }, food);
        }

        //Read - Single
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteFood>> GetFavoriteFood(int id) {
            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null) return NotFound();
            return food;
        }

        //Read - Multiple
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteFood>>> GetFavoriteFoods(int? id) {
            if (id == null || id == 0)
                return await _context.FavoriteFoods.Take(5).ToListAsync();

            var food = await _context.FavoriteFoods.FindAsync(id);
            return food == null ? NotFound() : new List<FavoriteFood> { food };
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavoriteFood(int id, FavoriteFood food) {
            if (id != food.ID) return BadRequest();
            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteFood(int id) {
            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null) return NotFound();
            _context.FavoriteFoods.Remove(food);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}