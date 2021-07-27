using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentTeendanceBackend.Data;

namespace StudentTeendanceBackend.Repository
{
    public class DatabaseRepository
    {
        protected StudentTeendanceBackendContext dbcontext = new StudentTeendanceBackendContext();
    }
}
