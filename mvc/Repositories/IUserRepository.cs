using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Repositories
{
    public class IUserRepository
    {
        public int Register(User user);

        public int Login(User user);
    }
}