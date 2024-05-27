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
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        public IActionResult GetSubjects()
        {
            var subjects = _mapper.Map<List<SubjectDto>>(_subjectRepository.GetSubjects());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subjects);
        }

        [HttpGet("{subjectId}")]
        [ProducesResponseType(200, Type = typeof(Subject))]
        [ProducesResponseType(400)]
        public IActionResult GetSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound();
            var subject = _mapper.Map<SubjectDto>(_subjectRepository.GetSubject(subjectId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subject);
        }
    }
}
