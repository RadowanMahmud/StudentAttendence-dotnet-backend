using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTeendanceBackend.Model;

namespace StudentTeendanceBackend.Data
{
    public class StudentTeendanceBackendContext : DbContext
    {
        public StudentTeendanceBackendContext (DbContextOptions<StudentTeendanceBackendContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<StudentTeendanceBackend.Model.Admin> Admin { get; set; }

        public DbSet<StudentTeendanceBackend.Model.Attendance> Attendance { get; set; }

        public DbSet<StudentTeendanceBackend.Model.Record> Record { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentTeendanceBackend.Model.Admin>().HasData(
                new Admin
                {
                    Id=1,
                    Name = "Admin One",
                    Email = "adminone@admin.com",
                    Designation = "Teacher",
                    Password = "iit786445",
                    Roles = "admin",
                }
            );
        }
    }
}
