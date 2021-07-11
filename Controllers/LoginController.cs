using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTeendanceBackend.ViewModel;
using StudentTeendanceBackend.Data;
using StudentTeendanceBackend.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTeendanceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IJwtAuthManager jwtAuthManager;
        private readonly StudentTeendanceBackendContext _context;

        public LoginController(IJwtAuthManager jwtAuth, StudentTeendanceBackendContext context)
        {
            this.jwtAuthManager = jwtAuth;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            if (userLogin.role == "admin") {
                var entity = _context.Admin.Where(p => p.Email == userLogin.email && p.Password == userLogin.password).FirstOrDefault();
                if (entity != null)
                {
                    var token = jwtAuthManager.Authenticate(userLogin.email, userLogin.password);
                    if (token == null) return Unauthorized();
                    else
                    {
                        MyData m = new MyData();
                        m.data = entity;
                        m.token = token;

                        return Ok(m);
                    }
                }
                return Unauthorized();
            }
            else if (userLogin.role == "student") {
                var entity = _context.Student.Where(p => p.Email == userLogin.email && p.Password == userLogin.password).FirstOrDefault();
                if (entity != null)
                {
                    var token = jwtAuthManager.Authenticate(userLogin.email, userLogin.password);
                    if (token == null) return Unauthorized();
                    else {
                        MyData m = new MyData();
                        m.data = entity;
                        m.token = token;

                        return Ok(m);
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        [HttpGet("name")]
        public IEnumerable<string> Get() {
            return new string[] {"value1", "value2"};
        }
    }

    public class MyData
    {
         public object data { get; set; }
         public string token { get; set; }
    }
}
