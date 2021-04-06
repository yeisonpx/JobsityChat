using JobSityChat.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSityChat.Core.Repositories
{
    interface IUserRepository
    {
        ApplicationUser GetByUserName(string userName);
        ApplicationUser GetById(string userId);
    }
}
