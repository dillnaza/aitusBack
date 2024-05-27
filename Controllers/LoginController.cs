using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using aitus.Interfaces;
using aitus.Models;
using aituss.Interfaces;

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
            // Проверка наличия студента с данным email и паролем
            var student = await _studentRepository.GetStudentByEmailAndPasswordAsync(request.Email, request.Password);
            if (student != null)
            {
                // Пользователь найден - перенаправляем на страницу StudentAttendanceController с указанием его Id
                return RedirectToAction("GetStudentAttendance", "StudentAttendance", new { studentId = student.StudentId });
            }

            // Проверка наличия учителя с данным email и паролем
            var teacher = await _teacherRepository.GetTeacherByEmailAndPasswordAsync(request.Email, request.Password);
            if (teacher != null)
            {
                // Пользователь найден - вернуть информацию о учителе
                return Ok(new { UserType = "Teacher", UserId = teacher.TeacherId });
            }

            // Если пользователь не найден или пароль неверный, вернуть сообщение об ошибке
            return BadRequest("Invalid email or password");
        }
    }
}
