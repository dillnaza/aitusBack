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
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Attendance>))]
        public IActionResult GetAttendances()
        {
            var attendances = _mapper.Map<List<AttendanceDto>>(_attendanceRepository.GetAttendances());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(attendances);
        }

        [HttpGet("{attendanceId}")]
        [ProducesResponseType(200, Type = typeof(Attendance))]
        [ProducesResponseType(400)]
        public IActionResult GetAttendance(int attendanceId)
        {
            if (!_attendanceRepository.AttendanceExist(attendanceId))
                return NotFound();
            var attendance = _mapper.Map<AttendanceDto>(_attendanceRepository.GetAttendance(attendanceId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(attendance);
        }
    }
}
