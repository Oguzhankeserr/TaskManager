using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Log.Entity;

namespace TaskManager.Business.LogService.Context
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) { }
        public DbSet<Log.Entity.Log> Logs { get; set; }
    }
}
