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

namespace StudentTeendanceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly StudentTeendanceBackendContext _context;

        public RecordsController(StudentTeendanceBackendContext context)
        {
            _context = context;
        }

        // GET: api/Records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecord()
        {
            return await _context.Record.ToListAsync();
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
            var record = await _context.Record.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        // PUT: api/Records/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, Record record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Records
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record record)
        {
            record.GivenTime = DateTime.Now;
            _context.Record.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = record.Id }, record);
        }

        // DELETE: api/Records/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> DeleteRecord(int id)
        {
            var record = await _context.Record.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.Record.Remove(record);
            await _context.SaveChangesAsync();

            return record;
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.Id == id);
        }

        [HttpGet("reports/daily/{studentId}")]
        public async Task<ActionResult<IEnumerable<Record>>> GetDailyRecordByStudentId(int studentId)
        {
            var query = DateTime.Now;

            var records = await _context.Record
                .Where(a => a.GivenTime.Day == query.Day && a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year && a.StudentId == studentId).ToListAsync();

            var listofMyrecords = new List<MyRecord>();
            
            foreach (var record in records)
            {
                var temp = new MyRecord();
                temp.record = record;
                var attendence = _context.Attendance.Where(p => p.Id == record.attendancesId).FirstOrDefault();
                temp.attendence = attendence;
                temp.admin = _context.Admin.Where(p => p.Id == attendence.AdminId).FirstOrDefault();

                listofMyrecords.Add(temp);
                
            }

            return Ok(listofMyrecords);
        }

        [HttpGet("reports/monthly/{month}/{studentId}")]
        public async Task<ActionResult<IEnumerable<Record>>> GetMonthlyRecordByStudentId(string month,int studentId)
        {
            var query = DateTime.Parse(month);

            var records = await _context.Record
                .Where(a => a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year && a.StudentId == studentId).ToListAsync();

            var listofMyrecords = new List<MyRecord>();

            foreach (var record in records)
            {
                var temp = new MyRecord();
                temp.record = record;
                var attendence = _context.Attendance.Where(p => p.Id == record.attendancesId).FirstOrDefault();
                temp.attendence = attendence;
                temp.admin = _context.Admin.Where(p => p.Id == attendence.AdminId).FirstOrDefault();

                listofMyrecords.Add(temp);

            }

            return Ok(listofMyrecords);
        }


    }

    public class MyRecord
    {
        public object record { get; set; }
        public object attendence { get; set; }
        public object admin { get; set; }
    }
}
