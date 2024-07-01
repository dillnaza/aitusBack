using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using aitus.Interfaces;
using aitus.Models;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public LoginController(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var student = await _studentRepository.GetStudentByEmailAndPasswordAsync(request.Email, request.Password);
            if (student != null)
            {
                return Ok(new { UserType = "Student", UserId = student.StudentId });
            }
            var teacher = await _teacherRepository.GetTeacherByEmailAndPasswordAsync(request.Email, request.Password);
            if (teacher != null)
            {
                return Ok(new { UserType = "Teacher", UserId = teacher.TeacherId });
            }
            return BadRequest("Invalid email or password");
        }
    }
}
