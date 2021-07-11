using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTeendanceBackend
{
    public interface IJwtAuthManager
    {
        string Authenticate(string email, string password);
    }
}
