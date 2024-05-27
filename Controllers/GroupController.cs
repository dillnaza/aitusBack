using aitus.Dto;
using aitus.Interfaces;
using aitus.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupController(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        public IActionResult GetGroups()
        {
            var groups = _mapper.Map<List<GroupDto>>(_groupRepository.GetGroups());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(groups);
        }

        [HttpGet("{groupId}/students")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetGroupStudents(int groupId)
        {
            if (!_groupRepository.GroupExists(groupId))
                return NotFound();
            var students = _groupRepository.GetGroupStudents(groupId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(students);
        }
    }
}