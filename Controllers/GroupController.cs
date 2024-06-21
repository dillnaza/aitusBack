using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using aitus.Dto;
using aitus.Interfaces;
using aitus.Models;

namespace aitus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupController(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GroupDto>))]
        public IActionResult GetGroups()
        {
            var groups = _groupRepository.GetGroups();
            var groupDtos = _mapper.Map<List<GroupDto>>(groups);
            return Ok(groupDtos);
        }

        [HttpGet("{groupId}")]
        [ProducesResponseType(200, Type = typeof(GroupDto))]
        [ProducesResponseType(404)]
        public IActionResult GetGroup(int groupId)
        {
            var group = _groupRepository.GetGroup(groupId);
            if (group == null)
                return NotFound();
            var groupDto = _mapper.Map<GroupDto>(group);
            return Ok(groupDto);
        }

        [HttpGet("{groupId}/students")]
        [ProducesResponseType(200, Type = typeof(List<StudentDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetGroupStudents(int groupId)
        {
            var groupExists = _groupRepository.GroupExists(groupId);
            if (!groupExists)
                return NotFound();
            var students = _groupRepository.GetGroupStudents(groupId);
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
        }
    }
}
