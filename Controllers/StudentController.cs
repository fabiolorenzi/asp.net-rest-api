using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Data;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var target = await _context.Students.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(target);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var studentToDelete = await _context.Students.FindAsync(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }

            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
