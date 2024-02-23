using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using Npgsql;

namespace mvc.Repositories
{
    public interface ITaskRepository
    {
        public List<MyTask> GetAllTask();
        public MyTask GetOneTask(int id);
        public void AddTask(MyTask task);
        public void AddToMyTask(MyTask myTask);
        public void UpdateStatus(MyTask myTask);

        public List<MyTask> GetMyTask();
        public List<MyTask> GetUsersTask();
        // private int GetTaskTypeId(string TaskType, NpgsqlConnection conn);
        public void EditTask(MyTask task);
        public void DeleteTask(MyTask task);
    }
}