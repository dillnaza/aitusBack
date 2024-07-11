using aitus.Dto;
using aitus.Interfaces;
using aitus.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var groupSubjects = _groupRepository.GetGroupSubjects(groupId);

            if (!groupSubjects.Any(gs => gs.SubjectId == subjectId && _teacherRepository.TeachesSubject(teacherId, gs.SubjectId)))
            {
                return NotFound($"The teacher with ID {teacherId} does not teach the subject with ID {subjectId} in the group with ID {groupId}.");
            }

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

            var groupSubjects = _groupRepository.GetGroupSubjects(groupId);

            if (!groupSubjects.Any(gs => gs.SubjectId == subjectId && _teacherRepository.TeachesSubject(teacherId, gs.SubjectId)))
            {
                return NotFound($"The teacher with ID {teacherId} does not teach the subject with ID {subjectId} in the group with ID {groupId}.");
            }

            var attendance = _attendanceRepository.GetAttendanceByDateAndSubjectId(date, subjectId);
            if (attendance == null)
            {
                return NotFound($"Attendance date {date} for subject with ID {subjectId} not found.");
            }

            var attendanceRecords = _attendanceRepository.GetAttendancesByGroupIdAndSubjectIdAndDate(groupId, subjectId, date).ToList();

            if (!attendanceRecords.Any())
            {
                return NotFound($"No attendance records found for date {date}, subject ID {subjectId}, group ID {groupId}, and teacher ID {teacherId}.");
            }

            var teacher = _teacherRepository.GetTeacher(teacherId);
            var teacherBarcode = _teacherRepository.GetTeacherBarcode(teacher.Email);
            var subject = _subjectRepository.GetSubject(subjectId);
            var group = _groupRepository.GetGroup(groupId);

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

        [HttpPost("{teacherId}/subject/{subjectId}/group/{groupId}/attendance-dates")]
        public IActionResult AddAttendanceDate(int teacherId, int subjectId, int groupId, [FromBody] DateTime date)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound($"Teacher with ID {teacherId} not found.");

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound($"Subject with ID {subjectId} not found.");

            if (!_groupRepository.GroupExists(groupId))
                return NotFound($"Group with ID {groupId} not found.");

            var groupSubjects = _groupRepository.GetGroupSubjects(groupId);

            if (!groupSubjects.Any(gs => gs.SubjectId == subjectId && _teacherRepository.TeachesSubject(teacherId, gs.SubjectId)))
            {
                return NotFound($"The teacher with ID {teacherId} does not teach the subject with ID {subjectId} in the group with ID {groupId}.");
            }

            var existingAttendance = _attendanceRepository.GetAttendanceByDateAndSubjectId(date, subjectId);
            if (existingAttendance != null)
            {
                return Conflict($"Attendance date {date} for subject with ID {subjectId} already exists.");
            }

            var attendance = new Attendance
            {
                Date = date,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _attendanceRepository.AddAttendance(attendance);
            _attendanceRepository.Save();

            var attendanceSubject = new AttendanceSubject
            {
                AttendanceId = attendance.AttendanceId,
                SubjectId = subjectId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _attendanceRepository.AddAttendanceSubject(attendanceSubject);
            _attendanceRepository.Save();

            var students = _groupRepository.GetGroupStudents(groupId);
            foreach (var student in students)
            {
                var attendanceStudent = new AttendanceStudent
                {
                    AttendanceId = attendance.AttendanceId,
                    StudentId = student.StudentId,
                    Status = AttendanceStatus.Present,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _attendanceRepository.AddAttendanceStudent(attendanceStudent);
            }

            _attendanceRepository.Save();

            return Ok();
        }

        [HttpDelete("{teacherId}/subject/{subjectId}/group/{groupId}/attendance-dates/{date}")]
        public IActionResult DeleteAttendanceDate(int teacherId, int subjectId, int groupId, DateTime date)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound($"Teacher with ID {teacherId} not found.");

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound($"Subject with ID {subjectId} not found.");

            if (!_groupRepository.GroupExists(groupId))
                return NotFound($"Group with ID {groupId} not found.");

            var groupSubjects = _groupRepository.GetGroupSubjects(groupId);

            if (!groupSubjects.Any(gs => gs.SubjectId == subjectId && _teacherRepository.TeachesSubject(teacherId, gs.SubjectId)))
            {
                return NotFound($"The teacher with ID {teacherId} does not teach the subject with ID {subjectId} in the group with ID {groupId}.");
            }

            var attendance = _attendanceRepository.GetAttendanceByDateAndSubjectId(date, subjectId);
            if (attendance == null)
            {
                return NotFound($"Attendance date {date} for subject with ID {subjectId} not found.");
            }

            _attendanceRepository.DeleteAttendance(attendance);
            _attendanceRepository.Save();

            return Ok();
        }
    }
}
