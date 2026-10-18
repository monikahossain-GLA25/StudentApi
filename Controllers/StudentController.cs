using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Models.Dtos;


namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : Controller
    {
        private readonly AppDbContext appContext;

        public StudentController(AppDbContext appContext)
        {
            this.appContext = appContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAll()
        {
            var students = await appContext.Students.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
            return Ok(students);
        }


        // GET: /api/students/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var student = await appContext.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> Create([FromBody] StudentRequest studentRequest)
        {
            var student = new Student
            {
                Name = studentRequest.Name.Trim(),
                Email = studentRequest.Email.Trim()
            };

            appContext.Students.Add(student);
            await appContext.SaveChangesAsync();

            return CreatedAtAction(
                    nameof(GetById),
                    new
                    {
                        id = student.Id,
                    }, student
                );
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentRequest studentRequest)
        {
            var std = await appContext.Students.FindAsync(id);
            if (std is null)
            {
                return NotFound();
            }

            std.Name = studentRequest.Name.Trim();
            std.Email = studentRequest.Email.Trim();

            await appContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete (int id)
        {
            var deleteStd = await appContext.Students.FindAsync(id);
            if (deleteStd is null)
            {
                return NotFound();
            }
            appContext.Students.Remove(deleteStd);
           await appContext.SaveChangesAsync();

            return NoContent();
        }


    }
    
}
