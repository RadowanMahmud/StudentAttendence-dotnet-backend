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
    public class RecordsController : ControllerBase
    {
       
        public readonly ReportRepository _context = new ReportRepository();

        // GET: api/Records
        [HttpGet]
        public IActionResult GetRecord()
        {
            var records = _context.getAllRecords();

            return Ok(records);
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public IActionResult GetRecord(int id)
        {
            var record = _context.getRecordById(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

    
        [HttpPost]
        public  IActionResult PostRecord(Record recordreq)
        {
            var record = _context.addRecord(recordreq);

            return Ok(record);
        }

        // DELETE: api/Records/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRecord(int id)
        {
            var record = _context.deleteRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        [HttpGet("reports/daily/{studentId}")]
        public IActionResult GetDailyRecordByStudentId(int studentId)
        {
            
            var listofMyrecords = _context.getDailyReport(studentId);

            return Ok(listofMyrecords);
        }

        [HttpGet("admin/reports/daily/{adminId}")]
        public IActionResult GetDailyReporyAdmin(int adminId)
        {

            var listofMyrecords = _context.getDailyReportForAdmin(adminId);

            return Ok(listofMyrecords);
        }

        [HttpGet("reports/monthly/{month}/{studentId}")]
        public IActionResult GetMonthlyRecordByStudentId(string month,int studentId)
        {
            var listofMyrecords = _context.getMonthlyReport(month,studentId);
            return Ok(listofMyrecords);
        }

        [HttpGet("admin/reports/monthly/{month}/{adminId}")]
        public IActionResult GetMonthlyRecordByAdmin(string month, int adminId)
        {
            var listofMyrecords = _context.getMonthlyReportAdmin(month, adminId);
            return Ok(listofMyrecords);
        }

        [HttpGet("reports/weekly/{studentId}")]
        public IActionResult GetMonthlyWeeklyByStudentId(int studentId)
        {
            
            var listofMyrecords = _context.getWeeklyReport(studentId);
            return Ok(listofMyrecords);
        }

        [HttpGet("admin/reports/weekly/{adminId}")]
        public IActionResult GetWeeklyReportAdmin(int adminId)
        {

            var listofMyrecords = _context.getWeeklyReportForAdmin(adminId);
            return Ok(listofMyrecords);
        }

        [HttpGet("admin/reports/customized/{startDate}/{endDate}/{adminId}")]
        public IActionResult GetCustomizedReportAdmin(string startDate,string endDate,int adminId)
        {
            var result = _context.getCustomizedReportForAdmin(startDate, endDate, adminId);
            return Ok(result);
        }

        [HttpGet("reports/customized/{startDate}/{endDate}/{studentId}")]
        public IActionResult GetCustomizedReportStudent(string startDate, string endDate, int studentId)
        {
            var result = _context.getCustomizedReport(startDate, endDate, studentId);
            return Ok(result);
        }

    }

}

