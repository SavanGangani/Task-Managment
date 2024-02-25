using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Repositories;
using mvc.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository _userRepositories;

        public UserApiController(IUserRepository userRepositories)
        {
            _userRepositories = userRepositories;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var rowCount = _userRepositories.Login(user);

            if (rowCount > 0)
            {
                return Ok("Login Successful");
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var rowCount = _userRepositories.Register(user);

            if (rowCount > 0)
            {
                return Ok("Registration Successful");
            }
            else
            {
                return BadRequest("Registration Failed");
            }
        }

        
    }
}