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
    public class StudentsController : ControllerBase
    {
        public readonly StudentRepository _context = new StudentRepository();

        // GET: api/Students
        [HttpGet]
        public IActionResult GetStudent()
        {
            var result = _context.getAllStudents();
            return Ok(result);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student =  _context.getStudentsById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            var result = _context.editStudent(id, student);

          
                if (result == null)
                {
                    return NotFound();
                }          

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            var result = _context.addStudent(student);

            return Ok(result);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.deleteStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

      
    }
}
