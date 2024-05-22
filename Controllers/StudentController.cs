using aitus.Models;
using aituss.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aituss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentConroller : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentConroller(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetStudents();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();
            var student = _studentRepository.GetStudent(studentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(student);
        }
    }
}
