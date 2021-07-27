using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTeendanceBackend.Model;

namespace StudentTeendanceBackend.Repository
{
    public class AttendenceRepository: DatabaseRepository
    {
        public List<MyAttendence> getAttendences() 
        {
            var listOfAttendence = new List<MyAttendence>();
            var results = dbcontext.Attendance.ToList();
            foreach (var result in results)
            {
                var temp = new MyAttendence();
                temp.data = result;
                temp.admin = dbcontext.Admin.Where(p => p.Id == result.AdminId).FirstOrDefault();

                listOfAttendence.Add(temp);
            }
            return listOfAttendence;
        }

        public dynamic addAttendence(Attendance attendence)
        {
            dbcontext.Attendance.Add(attendence);
            var result = dbcontext.SaveChanges();

            return result;
        }

        public List<MyAttendence> getCurrentAttendanceById(int id)
        {
            var listOfAttendence = new List<MyAttendence>();
            var date = DateTime.Now;
            var results = dbcontext.Attendance.Where(p => p.StartTime <= date && p.EndTime >= date).ToList();

            foreach (var result in results)
            {
                var record = dbcontext.Record.Where(p => p.StudentId == id && p.attendancesId == result.Id).Count();

                if (record == 0)
                {
                    var temp = new MyAttendence();
                    temp.data = result;
                    temp.admin = dbcontext.Admin.Where(p => p.Id == result.AdminId).FirstOrDefault();

                    listOfAttendence.Add(temp);
                }
            }
            return listOfAttendence;
        }

        public dynamic editAttendence(int id, Attendance attendance)
        {
            dbcontext.Entry(attendance).State = EntityState.Modified;

            try
            {
                 dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return attendance;

        }

        public dynamic deleteAttendence(int id) {

            var attendance =  dbcontext.Attendance.Find(id);
            if (attendance == null)
            {
                return null;
            }

            dbcontext.Attendance.Remove(attendance);
            dbcontext.SaveChangesAsync();

            return attendance;
        }
        private bool AttendanceExists(int id)
        {
            return dbcontext.Attendance.Any(e => e.Id == id);
        }
    }

    public class MyAttendence
    {
        public object data { get; set; }
        public object admin { get; set; }
    }
}
