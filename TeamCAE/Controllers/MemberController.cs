using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCAE.Tables;

namespace TeamCAE.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase {
        private readonly TeamCAEDbContext _context;

        public MemberController(TeamCAEDbContext context) {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateMember(Member member) {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMember), new { id = member.ID }, member);
        }

        //Read - Single
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id) {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();
            return member;
        }

        //Read - Multiple
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers(int? id) {
            if (id == null || id == 0)
                return await _context.Members.Take(5).ToListAsync();

            var member = await _context.Members.FindAsync(id);
            return member == null ? NotFound() : new List<Member> { member };
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member) {
            if (id != member.ID) return BadRequest();
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id) {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}