using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentTeendanceBackend.Model;
using StudentTeendanceBackend.ViewModel;

namespace StudentTeendanceBackend.Repository
{
    public class AdminRepository: DatabaseRepository
    {
        public List<Admin> getAllAdmins()
        {
            return dbcontext.Admin.ToList();
        }

        public Admin login([FromBody] UserLogin userLogin)
        {
            return dbcontext.Admin.Where(p => p.Email == userLogin.email && p.Password == userLogin.password).FirstOrDefault();
        }

        public Admin getAdminById(int id)
        {
            var admin = dbcontext.Admin.Find(id);

            return admin;
        }

        public dynamic addAdmin(Admin admin) {
            dbcontext.Admin.Add(admin);
            var result = dbcontext.SaveChanges();

            return result;
        }

        public dynamic deleteAdmin(int id)
        {
            var admin =  dbcontext.Admin.Find(id);
            if (admin == null)
            {
                return null;
            }

            dbcontext.Admin.Remove(admin);
            return  dbcontext.SaveChangesAsync();
        }
    }
}
