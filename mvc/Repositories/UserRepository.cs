using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using mvc.Models;
using Npgsql;
using NpgsqlTypes;

namespace mvc.Repositories
{
    public class UserRepository : CommanRepository, IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int Login(User user)
        {
            int rowCount = 0;
            string role = "";
            string username = "";
            int studentID = 0, UserType = 0;

            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                try
                {
                    conn.Open();

                    using (var command = new NpgsqlCommand("SELECT c_role, c_usertype, c_username, COUNT(*) FROM t_taskuser WHERE c_email = @email AND c_password = @password GROUP BY c_role, c_usertype, c_role, c_username", conn))
                    {
                        command.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = user.c_email;
                        command.Parameters.Add("@password", NpgsqlDbType.Varchar).Value = user.c_password;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserType = reader.GetInt32(0);
                                role = reader.GetString(1);
                                username = reader.GetString(2);
                                rowCount = reader.GetInt32(3);

                                var session = _httpContextAccessor.HttpContext.Session;
                                session.SetInt32("userRole", UserType);
                                session.SetString("role", role);
                                session.SetString("username", username);
                                session.SetInt32("userType", UserType);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, log it, or rethrow if necessary
                    Console.WriteLine("An error occurred while opening the connection: " + ex.Message);
                }
            }

            return rowCount;
        }

        public int Register(User user)
        {
            int rowCount = 0;

            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    int roleId = GetTaskTypeId(user.c_role, conn);

                    using (var cmd = new NpgsqlCommand("INSERT INTO t_taskuser(c_username, c_email, c_password, c_usertype, c_role) VALUES (@c_username, @c_email, @c_password, @c_usertype, @c_role)", conn))
                    {
                        cmd.Parameters.AddWithValue("@c_username", user.c_username);
                        cmd.Parameters.AddWithValue("@c_email", user.c_email);
                        cmd.Parameters.AddWithValue("@c_password", user.c_password);
                        cmd.Parameters.AddWithValue("@c_usertype", user.c_usertype);
                        cmd.Parameters.AddWithValue("@c_role", roleId);

                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, log it, or rethrow if necessary
                    Console.WriteLine("An error occurred while executing the query: " + ex.Message);
                }
            }

            return rowCount;
        }

        private int GetTaskTypeId(string TaskType, NpgsqlConnection conn)
        {
            int tasktypeid = 0;

            using (var cmd = new NpgsqlCommand("SELECT c_tasktypeid FROM t_tasktype WHERE c_tasktype = @tasktype", conn))
            {
                cmd.Parameters.AddWithValue("@tasktype", TaskType);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tasktypeid = reader.GetInt32(0);
                    }
                }
            }

            return tasktypeid;
        }
    }
}
