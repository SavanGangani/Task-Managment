using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using Npgsql;

namespace mvc.Repositories
{
    public class UserRepository : CommanRepository , IUserRepository
    {
        public bool Login(User user)
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM t_taskuser WHERE c_email= @email AND c_password=@password";

                var cmd = new NpgsqlCommand(query,conn);

                cmd.Parameters.AddWithValue("@email", user.c_email);
                cmd.Parameters.AddWithValue("@password", user.c_password);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (System.Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}