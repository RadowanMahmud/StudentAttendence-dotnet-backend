using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeendanceBackend.Data;
using StudentTeendanceBackend.Model;
using StudentTeendanceBackend.Repository;

namespace StudentTeendanceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        public readonly AttendenceRepository _context = new AttendenceRepository();

        // GET: api/Attendances
        [HttpGet]
        public IActionResult GetAttendance()
        {
            var listOfAttendence = _context.getAttendences();
            return Ok(listOfAttendence);
        }

        [HttpGet("current/{id}")]
        public IActionResult GetCurrentAttendanceById(int id)
        {
            var listOfAttendence = _context.getCurrentAttendanceById(id);
            return Ok(listOfAttendence);
        }

        // PUT: api/Attendances/5
        [HttpPut("{id}")]
        public IActionResult PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            var result = _context.editAttendence(id, attendance);

            return NoContent();
        }

        // POST: api/Attendances
        [HttpPost]
        public IActionResult PostAttendance(Attendance attendance)
        {
         
            var result =  _context.addAttendence(attendance);
            return Ok(result);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAttendance(int id)
        {
            var attendance = _context.deleteAttendence(id);

            return Ok(attendance);
        }
    }

    public class MyAttendence
    {
        public object data { get; set; }
        public object admin { get; set; }
    }
}
