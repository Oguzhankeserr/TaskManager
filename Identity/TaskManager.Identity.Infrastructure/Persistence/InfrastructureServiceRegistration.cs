using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Entities;
using TaskManager.Identity.Infrastructure.Context;

namespace TaskManager.Identity.Infrastructure.Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagerDbContext>(options => options.UseNpgsql
                (configuration.GetConnectionString("BuySellConnection")));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }
            ).AddEntityFrameworkStores<TaskManagerDbContext>();
        }

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
