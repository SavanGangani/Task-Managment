using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskApiController : ControllerBase
    {
        private readonly ITaskRepository _taskRepositories;

        public TaskApiController(ITaskRepository taskRepositories)
        {
            _taskRepositories = taskRepositories;
        }

        [HttpGet]
        [Route("getAllTasks")]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskRepositories.GetAllTask();
            return Ok(tasks);
        }

        [HttpGet("getOneTask/{id}")]
        public IActionResult GetOneTask(int id)
        {
            var task = _taskRepositories.GetOneTask(id);

            if (task != null)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("addTask")]
        public IActionResult AddTask([FromBody] MyTask task)
        {
            _taskRepositories.AddTask(task);
            return Ok("Task added successfully");
        }

        [HttpPost("addToMyTask")]
        public IActionResult AddToMyTask([FromBody] MyTask myTask)
        {
            _taskRepositories.AddToMyTask(myTask);
            return Ok("Task added to user successfully");
        }

        [HttpPost("updateStatus")]
        public IActionResult UpdateStatus([FromBody] MyTask myTask)
        {
            _taskRepositories.UpdateStatus(myTask);
            return Ok("Task status updated successfully");
        }

        [HttpGet("getMyTasks")]
        public IActionResult GetMyTasks()
        {
            var tasks = _taskRepositories.GetMyTask();
            return Ok(tasks);
        }

        [HttpGet("getUsersTasks")]
        public IActionResult GetUsersTasks()
        {
            var tasks = _taskRepositories.GetUsersTask();
            return Ok(tasks);
        }

        [HttpDelete("deleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _taskRepositories.GetOneTask(id);

            if (task != null)
            {
                _taskRepositories.DeleteTask(task);
                return Ok("Task deleted successfully");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("editTask")]
        public IActionResult EditTask([FromBody] MyTask task)
        {
            _taskRepositories.EditTask(task);
            return Ok("Task edited successfully");
        }
    }
}