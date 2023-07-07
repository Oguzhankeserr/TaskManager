using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Infrastructure.Context
{
    public class TaskManagerDbContext : IdentityDbContext<AppUser,AppRole, string>
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
