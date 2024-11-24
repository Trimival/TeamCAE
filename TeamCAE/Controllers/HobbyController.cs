using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCAE.Tables;

namespace TeamCAE.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase {
        private readonly TeamCAEDbContext _context;

        public HobbyController(TeamCAEDbContext context) {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateHobby(Hobby hobby) {
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHobby), new { id = hobby.ID }, hobby);
        }

        //Read - Single
        [HttpGet("{id}")]
        public async Task<ActionResult<Hobby>> GetHobby(int id) {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null) return NotFound();
            return hobby;
        }

        //Read - Multiple
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbies(int? id) {
            if (id == null || id == 0)
                return await _context.Hobbies.Take(5).ToListAsync();

            var hobby = await _context.Hobbies.FindAsync(id);
            return hobby == null ? NotFound() : new List<Hobby> { hobby };
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHobby(int id, Hobby hobby) {
            if (id != hobby.ID) return BadRequest();
            _context.Entry(hobby).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id) {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null) return NotFound();
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}