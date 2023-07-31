using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Application
{
    public static class ApplicationBusinessServiceRegistration
    {
        public static void AddApplicationBusinessServices(this IServiceCollection services)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IRepository<Project>, Repository<Project>>();
            services.AddScoped<GenericService<Project>>();

        }
    }
}
