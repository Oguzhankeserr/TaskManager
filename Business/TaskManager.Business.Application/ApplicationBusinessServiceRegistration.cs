using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure;
using MediatR;
using TaskManager.Business.Application.Features;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application
{
    public static class ApplicationBusinessServiceRegistration
    {
        public static void AddApplicationBusinessServices(this IServiceCollection services)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IRepository<Project>, Repository<Project>>();

        }
    }
}
