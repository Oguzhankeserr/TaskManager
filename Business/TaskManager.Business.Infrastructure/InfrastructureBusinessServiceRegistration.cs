using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Infrastructure.Context;

namespace TaskManager.Business.Infrastructure
{
    public static class InfrastructureBusinessServiceRegistration
    {
        public static void AddBusinessInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessDbContext>(options => options.UseNpgsql
                (configuration.GetConnectionString("TaskManagerBusinessConnection")));
        }

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<BusinessDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
