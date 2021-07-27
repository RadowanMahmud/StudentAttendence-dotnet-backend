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
    public class AdminsController : ControllerBase
    {
 
        public readonly AdminRepository _adminRepository = new AdminRepository();


        // GET: api/admins
        [HttpGet]
        public IActionResult GetAdmin()
        {
            var admins = _adminRepository.getAllAdmins();

            return Ok(admins);
        }

        // GET: api/admins/5
        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _adminRepository.getAdminById(id);

            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPut("{id}")]
        public IActionResult PutAdmin(int id, Admin admin)
        {
            if (id != admin.Id)
            {
                return BadRequest();
            }

            var result = _adminRepository.editAdmin(id, admin);


            if (result == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }


        // POST: api/Admins  
        [HttpPost("register")]
        public IActionResult PostAdmin(Admin admin)
        {
           var result = _adminRepository.addAdmin(admin);

            return Ok(result);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _adminRepository.deleteAdmin(id);
            if(admin == null)
            {
                return NotFound();
            }

            return admin;
        }

    }
}
