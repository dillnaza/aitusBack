using aitus.Dto;
using aitus.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMainController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public StudentMainController(
            IStudentRepository studentRepository,
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IAttendanceRepository attendanceRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(StudentSubjectDto))]
        [ProducesResponseType(404)]
        public ActionResult<StudentSubjectDto> GetStudentSubjects(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var student = _studentRepository.GetStudent(studentId);
            var studentBarcode = _studentRepository.GetStudentBarcode(student.Email);
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

            var studentAttendanceDto = new StudentSubjectDto
            {
                StudentBarcode = studentBarcode,
                StudentName = student.Name,
                StudentSurname = student.Surname,
                GroupName = group.GroupName,
                SubjectTeacher = subjectTeacher
            };

            return Ok(studentAttendanceDto);
        }

        [HttpGet("student/{studentId}/subject/{subjectId}")]
        public ActionResult<StudentAttendanceDto> GetStudentAttendance(int studentId, int subjectId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound($"Student with ID {studentId} not found.");

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound($"Subject with ID {subjectId} not found.");

            var student = _studentRepository.GetStudent(studentId);
            var group = _groupRepository.GetGroup(student.GroupId);
            var subject = _subjectRepository.GetSubject(subjectId);
            var subjectTeacher = _teacherRepository.GetTeacherNameBySubjectAndGroup(subjectId, group.GroupId);
            var studentBarcode = _studentRepository.GetStudentBarcode(student.Email);
            var attendanceRecords = _attendanceRepository.GetAttendancesByStudentIdAndSubject(studentId, subjectId);
            var attendancePercent = _attendanceRepository.GetAttendancePercent(studentId, subjectId);

            // Map AttendanceStudent to AttendanceDto
            var attendanceDtos = attendanceRecords.Select(ar => new AttendanceDto
            {
                Date = ar.Attendance.Date,
                Status = ar.Status.ToString() // Map the status correctly
            }).ToList();

            var studentAttendanceDto = new StudentAttendanceDto
            {
                StudentBarcode = studentBarcode,
                StudentName = student.Name,
                StudentSurname = student.Surname,
                GroupName = group.GroupName,
                TeacherName = subjectTeacher.Name,
                TeacherSurname = subjectTeacher.Surname,
                SubjectName = subject.SubjectName,
                AttendancePercent = attendancePercent,
                Attendances = attendanceDtos
            };

            return Ok(studentAttendanceDto);
        }

    }
}
