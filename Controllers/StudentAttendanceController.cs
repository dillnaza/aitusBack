using aitus.Dto;
using aitus.Interfaces;
using aituss.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public StudentAttendanceController(
            IStudentRepository studentRepository,
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(StudentAttendanceDto))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentAttendance(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var student = _studentRepository.GetStudent(studentId);
            var group = _groupRepository.GetGroup(student.GroupId);

            var groupSubjects = _groupRepository.GetGroupSubjects(student.GroupId);
            var subjectTeacher = new List<SubjectTeacherDto>();

            foreach (var groupSubject in groupSubjects)
            {
                var subject = _subjectRepository.GetSubject(groupSubject.SubjectId);
                var groupTeachers = _groupRepository.GetGroupTeachers(student.GroupId);
                foreach (var groupTeacher in groupTeachers)
                {
                    var teacher = _teacherRepository.GetTeacher(groupTeacher.TeacherId);
                    if (_teacherRepository.TeachesSubject(teacher.TeacherId, subject.SubjectId))
                    {
                        subjectTeacher.Add(new SubjectTeacherDto
                        {
                            SubjectName = subject.SubjectName,
                            TeacherName = $"{teacher.Name} {teacher.Surname}"
                        });
                    }
                }
            }

            var studentBarcode = _studentRepository.GetStudentBarcode(student.Email);

            var studentAttendanceDto = new StudentAttendanceDto
            {
                StudentBarcode = studentBarcode,
                GroupName = group.GroupName,
                SubjectTeacher = subjectTeacher
            };

            return Ok(studentAttendanceDto);
        }
    }
}
