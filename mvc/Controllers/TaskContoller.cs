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
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        public IActionResult Index()
        {
            // Console.WriteLine(HttpContext.Session.GetString("username"));
            if (HttpContext.Session.GetString("role") == "admin")
            {
                var tasks = _taskRepository.GetAllTask();
                return View(tasks);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            MyTask task = _taskRepository.GetOneTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }



        [HttpPost]

        public IActionResult Add(MyTask task)
        {
            task.c_taskusername = "default_username";
            _taskRepository.AddTask(task);
            return RedirectToAction("Index");

        }

        public IActionResult AddToMyTask(int id)
        {
            MyTask addtoMyTask = _taskRepository.GetOneTask(id);
            _taskRepository.AddToMyTask(addtoMyTask);
            return RedirectToAction("MyTask");
        }

        public IActionResult UpdateStatus(int id)
        {
            if (HttpContext.Session.GetString("username") != "")
            {
                MyTask updateStatus = _taskRepository.GetOneTask(id);
                _taskRepository.UpdateStatus(updateStatus);
                return RedirectToAction("MyTask");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        public IActionResult Delete(int id)
        {
            MyTask taskToDelete = _taskRepository.GetOneTask(id);
            if (taskToDelete == null)
            {
                return NotFound(); // Return 404 if task not found
            }
            _taskRepository.DeleteTask(taskToDelete);
            return RedirectToAction("Index");
        }

        public IActionResult MyTask()
        {
            if (HttpContext.Session.GetString("username") != "")
            {
                List<MyTask> taskList = _taskRepository.GetMyTask();
                return View(taskList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult TaskManager()
        {
            if (HttpContext.Session.GetString("username") != "")
            {
                List<MyTask> taskList = _taskRepository.GetUsersTask();
                return View(taskList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            MyTask task = _taskRepository.GetOneTask(id);
            if (task == null)
            
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Update(MyTask task)
        {
            _taskRepository.EditTask(task);
            return RedirectToAction("Index", "Task");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}