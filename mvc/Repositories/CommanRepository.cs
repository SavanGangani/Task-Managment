using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Repositories
{
    public class CommanRepository
    {
         public CommanRepositories()
        {
                IConfiguration myConfig = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                                            

                conn = new NpgsqlConnection(myConfig.GetConnectionString("Myconnection"));
        }
    }
}