using aitus.Dto;
using aitus.Interfaces;
using aitus.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(teachers);
        }

        [HttpGet("{teacherId}")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();
            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teacherId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(teacher);
        }
    }
}
