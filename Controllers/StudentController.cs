using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;


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
            if(student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

                


    }
    
}
