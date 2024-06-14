using aitus.Interfaces;
using aitus.Dto;
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

        public TeacherMainController(
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("{teacherId}")]
        public IActionResult GetTeacherDetails(int teacherId)
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
    }
}
