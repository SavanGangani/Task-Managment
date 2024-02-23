using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using mvc.Repositories;


namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        // private readonly UserHelper _userHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository; 

        public UserController( IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            // _logger = logger;
            // _userHelper = userHelper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
            HttpContext.Session.SetString("username", "");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            int rowcount = _userRepository.Login(user);
            if (rowcount == 1)
            {
                if (HttpContext.Session.GetString("role") == "admin")
                {
                    Console.WriteLine(HttpContext.Session.GetString("role"));
                    return RedirectToAction("Index", "Task");
                }
                else
                {
                    Console.WriteLine(HttpContext.Session.GetString("role"));
                    return RedirectToAction("TaskManager", "Task");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
            // return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _userRepository.Register(user);
            return RedirectToAction("Index", "Task");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}



// Context.Session.GetString