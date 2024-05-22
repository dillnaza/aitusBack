using aitus.Interfaces;
using aitus.Models;
using aituss.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupReposotiry _groupRepository;

        public GroupController(IGroupReposotiry groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        public IActionResult GetGroups()
        {
            var groups = _groupRepository.GetGroups();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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