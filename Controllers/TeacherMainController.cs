using aitus.Dto;
using aitus.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherMainController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public TeacherMainController(
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IAttendanceRepository attendanceRepository,
            IMapper mapper)
        {
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpGet("{teacherId}")]
        public IActionResult GetTeacherSubjects(int teacherId)
        {
            var teacher = _teacherRepository.GetTeacher(teacherId);
            if (teacher == null)
                return NotFound($"Teacher with ID {teacherId} not found.");
            var groups = _teacherRepository.GetGroupTeacherGroups(teacherId).ToList();
            var subjects = _teacherRepository.GetTeacherSubjects(teacherId)
                .Select(gs => gs.Subject)
                .Distinct()
                .ToList();
            var teacherBarcode = _teacherRepository.GetTeacherBarcode(teacher.Email);
            var subjectGroupList = new List<SubjectGroupDto>();
            foreach (var group in groups)
            {
                var groupSubjects = _groupRepository.GetGroupSubjects(group.GroupId)
                    .Where(gs => subjects.Any(s => s.SubjectId == gs.SubjectId))
                    .Select(gs => new SubjectGroupDto
                    {
                        GroupName = group.GroupName,
                        SubjectName = gs.Subject.SubjectName
                    });
                subjectGroupList.AddRange(groupSubjects);
            }
            var teacherDto = new TeacherSubjectDto
            {
                TeacherBarcode = teacherBarcode,
                TeacherName = teacher.Name,
                TeacherSurname = teacher.Surname,
                SubjectGroup = subjectGroupList
            };

            return Ok(teacherDto);
        }

        [HttpGet("{teacherId}/subject/{subjectId}/group/{groupId}/attendance-dates")]
        public IActionResult GetAttendanceDatesByGroupIdAndSubjectId(int teacherId, int subjectId, int groupId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound($"Teacher with ID {teacherId} not found.");

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound($"Subject with ID {subjectId} not found.");

            if (!_groupRepository.GroupExists(groupId))
                return NotFound($"Group with ID {groupId} not found.");

            var teacher = _teacherRepository.GetTeacher(teacherId);
            var teacherBarcode = _teacherRepository.GetTeacherBarcode(teacher.Email);
            var subject = _subjectRepository.GetSubject(subjectId);
            var group = _groupRepository.GetGroup(groupId);
            var attendanceDates = _attendanceRepository.GetAttendanceDatesByGroupIdAndSubjectId(groupId, subjectId).ToList();

            var teacherAttendanceDto = new TeacherAttendanceDto
            {
                TeacherBarcode = teacherBarcode,
                TeacherName = teacher.Name,
                TeacherSurname = teacher.Surname,
                SubjectName = subject.SubjectName,
                GroupName = group.GroupName,
                AttendanceDates = attendanceDates
            };

            return Ok(teacherAttendanceDto);
        }

        [HttpGet("{teacherId}/subject/{subjectId}/group/{groupId}/attendances/{date}")]
        public IActionResult GetStudentAttendances(int teacherId, int subjectId, int groupId, DateTime date)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound($"Teacher with ID {teacherId} not found.");

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound($"Subject with ID {subjectId} not found.");

            if (!_groupRepository.GroupExists(groupId))
                return NotFound($"Group with ID {groupId} not found.");

            var teacher = _teacherRepository.GetTeacher(teacherId);
            var teacherBarcode = _teacherRepository.GetTeacherBarcode(teacher.Email);
            var subject = _subjectRepository.GetSubject(subjectId);
            var group = _groupRepository.GetGroup(groupId);
            var attendanceRecords = _attendanceRepository.GetAttendancesByGroupIdAndSubjectIdAndDate(groupId, subjectId, date).ToList();

            var studentAttendances = attendanceRecords.Select(ar => new StudentAttendanceStatusDto
            {
                StudentName = ar.Student.Name,
                Status = ar.Status.ToString()
            }).ToList();

            var studentAttendanceDto = new StudentAttendancesDto
            {
                TeacherBarcode = teacherBarcode,
                TeacherName = teacher.Name,
                TeacherSurname = teacher.Surname,
                SubjectName = subject.SubjectName,
                GroupName = group.GroupName,
                Date = date,
                StudentAttendances = studentAttendances
            };

            return Ok(studentAttendanceDto);
        }
    }
}
