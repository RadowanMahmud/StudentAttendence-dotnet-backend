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
            record.GivenTime = DateTime.Now;
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

            dbcontext.Record.Remove(record);
            dbcontext.SaveChangesAsync();

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


        public List<MyAdminRecord> getDailyReportForAdmin(int adminId)
        {
            var query = DateTime.Now;

            var records = dbcontext.Record
                .Where(a => a.GivenTime.Day == query.Day && a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year).ToList();

            var listofMyrecords = new List<MyAdminRecord>();

            foreach (var record in records)
            {
                var temp = new MyAdminRecord();
                temp.record = record;
                var attendencecount = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).Count();
                if (attendencecount > 0) {
                    var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).FirstOrDefault();
                    temp.attendence = attendence;
                    temp.student = dbcontext.Student.Where(p => p.Id == record.StudentId).FirstOrDefault();

                    listofMyrecords.Add(temp);
                }
            }

            return listofMyrecords;
        }

        public List<MyAdminRecord> getMonthlyReportAdmin(string month, int adminId)
        {
            var query = DateTime.Parse(month);

            var records = dbcontext.Record
                .Where(a => a.GivenTime.Month == query.Month && a.GivenTime.Year == query.Year).ToList();

            var listofMyrecords = new List<MyAdminRecord>();

            foreach (var record in records)
            {
                var temp = new MyAdminRecord();
                temp.record = record;
                var attendencecount = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).Count();
                if (attendencecount > 0)
                {
                    var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).FirstOrDefault();
                    temp.attendence = attendence;
                    temp.student = dbcontext.Student.Where(p => p.Id == record.StudentId).FirstOrDefault();

                    listofMyrecords.Add(temp);
                }

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

        public List<MyAdminRecord> getWeeklyReportForAdmin(int adminId)
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
            int daysFromCurrentDay = DayOfWeek.Saturday - currentDay;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
            DateTime currentWeekEndDate = DateTime.Now.AddDays(daysFromCurrentDay);


            var records = dbcontext.Record
                .Where(a => a.GivenTime.Date >= currentWeekStartDate.Date && a.GivenTime.Date <= currentWeekEndDate.Date).ToList();

            var listofMyrecords = new List<MyAdminRecord>();

            foreach (var record in records)
            {
                var temp = new MyAdminRecord();
                temp.record = record;
                var attendencecount = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).Count();
                if (attendencecount > 0)
                {
                    var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).FirstOrDefault();
                    temp.attendence = attendence;
                    temp.student = dbcontext.Student.Where(p => p.Id == record.StudentId).FirstOrDefault();

                    listofMyrecords.Add(temp);
                }

            }
            return listofMyrecords;
        }


        public List<MyAdminRecord> getCustomizedReportForAdmin(string startTime,string endTime,int adminId)
        {
            DateTime currentWeekStartDate = DateTime.Parse(startTime);
            DateTime currentWeekEndDate = DateTime.Parse(endTime);


            var records = dbcontext.Record
                .Where(a => a.GivenTime.Date >= currentWeekStartDate.Date && a.GivenTime.Date <= currentWeekEndDate.Date).ToList();

            var listofMyrecords = new List<MyAdminRecord>();

            foreach (var record in records)
            {
                var temp = new MyAdminRecord();
                temp.record = record;
                var attendencecount = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).Count();
                if (attendencecount > 0)
                {
                    var attendence = dbcontext.Attendance.Where(p => p.Id == record.attendancesId && p.AdminId == adminId).FirstOrDefault();
                    temp.attendence = attendence;
                    temp.student = dbcontext.Student.Where(p => p.Id == record.StudentId).FirstOrDefault();

                    listofMyrecords.Add(temp);
                }

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

    public class MyAdminRecord
    {
        public object record { get; set; }
        public object attendence { get; set; }
        public object student { get; set; }
    }
}
