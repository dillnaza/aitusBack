using aitus.Dto;
using aitus.Models;
using aituss.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace aituss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentConroller : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentConroller(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
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
            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(student);
        }
    }
}
