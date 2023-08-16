using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;
using Dapper;

namespace TaskManager.Business.Infrastructure.Context
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set;}
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<TaskFile> TaskFiles { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public List<T> ExecuteQuery<T>(string querty, object param = null)
        {
            return this.Database.GetDbConnection().QueryAsync<T>(querty, param).Result.ToList();
        }
      
    }
}
