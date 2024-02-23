using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Repositories
{
    public interface IUserRepository
    {
        public int Login(User user);
        public int Register(User user);
    }
}