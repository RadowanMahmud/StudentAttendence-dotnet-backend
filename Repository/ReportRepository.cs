using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentTeendanceBackend.Data;

namespace StudentTeendanceBackend.Repository
{
    public class ReportRepository
    {
        private readonly StudentTeendanceBackendContext _context;

        public ReportRepository(StudentTeendanceBackendContext context)
        {
            _context = context;
        }

    }

    public class MyRecord
    {
        public object record { get; set; }
        public object attendence { get; set; }
        public object admin { get; set; }
    }
}
