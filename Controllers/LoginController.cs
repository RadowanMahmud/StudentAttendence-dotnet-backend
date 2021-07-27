using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTeendanceBackend.ViewModel;
using StudentTeendanceBackend.Data;
using StudentTeendanceBackend.Model;
using StudentTeendanceBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTeendanceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IJwtAuthManager jwtAuthManager;
        public readonly AdminRepository _adminRepository = new AdminRepository();
        public readonly StudentRepository _studentRepository = new StudentRepository();



        public LoginController(IJwtAuthManager jwtAuth)
        {
            this.jwtAuthManager = jwtAuth;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            if (userLogin.role == "admin") {
                var entity = _adminRepository.login(userLogin);
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
                var entity = _studentRepository.login(userLogin);
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
