using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentTeendanceBackend.Data;
using StudentTeendanceBackend.Model;

namespace StudentTeendanceBackend.Repository
{
    public class ReportRepository: DatabaseRepository
    {
        public List<Record> getAllRecords()
        {
            return dbcontext.Record.ToList();
        }

        public Record getRecordById(int id)
        {
            var record = dbcontext.Record.Find(id);

            return record;
        }

        public dynamic addRecord(Record record)
        {
            dbcontext.Record.Add(record);
            var result = dbcontext.SaveChanges();

            return result;
        }

        public dynamic deleteRecord(int id)
        {
            var record = dbcontext.Record.Find(id);
            if (record == null)
            {
                return null;
            }

            return record;
        }

        public List<MyRecord> getDailyReport(int studentId) 
        {
            var query = DateTime.Now;

            var records =  dbcontext.Record
                .Where(a => a.GivenTime.Day == query.Day && a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year && a.StudentId == studentId).ToList();

            var listofMyrecords = new List<MyRecord>();

            foreach (var record in records)
            {
                var temp = new MyRecord();
                temp.record = record;
                var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId).FirstOrDefault();
                temp.attendence = attendence;
                temp.admin = dbcontext.Admin.Where(p => p.Id == attendence.AdminId).FirstOrDefault();

                listofMyrecords.Add(temp);

            }

            return listofMyrecords;
        }

        public List<MyRecord> getMonthlyReport(string month, int studentId)
        {
            var query = DateTime.Parse(month);

            var records = dbcontext.Record
                .Where(a => a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year && a.StudentId == studentId).ToList();

            var listofMyrecords = new List<MyRecord>();

            foreach (var record in records)
            {
                var temp = new MyRecord();
                temp.record = record;
                var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId).FirstOrDefault();
                temp.attendence = attendence;
                temp.admin = dbcontext.Admin.Where(p => p.Id == attendence.AdminId).FirstOrDefault();

                listofMyrecords.Add(temp);

            }

            return listofMyrecords;
        }

        public List<MyRecord> getWeeklyReport(int studentId) 
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
            int daysFromCurrentDay = DayOfWeek.Saturday - currentDay;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
            DateTime currentWeekEndDate = DateTime.Now.AddDays(daysFromCurrentDay);


            var records = dbcontext.Record
                .Where(a => a.GivenTime.Date >= currentWeekStartDate.Date && a.GivenTime.Date <= currentWeekEndDate.Date && a.StudentId == studentId).ToList();

            var listofMyrecords = new List<MyRecord>();

            foreach (var record in records)
            {
                var temp = new MyRecord();
                temp.record = record;
                var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId).FirstOrDefault();
                temp.attendence = attendence;
                temp.admin = dbcontext.Admin.Where(p => p.Id == attendence.AdminId).FirstOrDefault();

                listofMyrecords.Add(temp);

            }
            return listofMyrecords;
        }

        private bool RecordExists(int id)
        {
            return dbcontext.Record.Any(e => e.Id == id);
        }

    }

    public class MyRecord
    {
        public object record { get; set; }
        public object attendence { get; set; }
        public object admin { get; set; }
    }
}
