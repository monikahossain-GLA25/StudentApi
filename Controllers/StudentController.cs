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

    }
    
}
