using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeendanceBackend.Model;
using StudentTeendanceBackend.ViewModel;

namespace StudentTeendanceBackend.Repository
{
    public class StudentRepository: DatabaseRepository
    {
        public List<Student> getAllStudents()
        {
            return dbcontext.Student.ToList();
        }

        public Student login([FromBody] UserLogin userLogin)
        {
            return dbcontext.Student.Where(p => p.Email == userLogin.email && p.Password == userLogin.password).FirstOrDefault();
        }

        public Student getStudentsById(int id)
        {
            var student = dbcontext.Student.Find(id);

            return student;
        }

        public dynamic addStudent(Student student)
        {
            dbcontext.Student.Add(student);
            var result = dbcontext.SaveChanges();

            return result;
        }

        public dynamic editStudent(int id, Student student)
        {
            dbcontext.Entry(student).State = EntityState.Modified;

            try
            {
                dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return student;

        }

        public dynamic deleteStudent(int id)
        {
            var student = dbcontext.Student.Find(id);
            if (student == null)
            {
                return null;
            }

            dbcontext.Student.Remove(student);

            var records = dbcontext.Record.Where(p => p.StudentId == id).ToList();

            foreach (var record in records)
            {
                // var record = dbcontext.Record.Find(id);

                dbcontext.Record.Remove(record);
            }

            dbcontext.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return dbcontext.Student.Any(e => e.Id == id);
        }
    }
}
