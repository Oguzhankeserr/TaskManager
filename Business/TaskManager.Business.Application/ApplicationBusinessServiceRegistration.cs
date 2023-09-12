using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure;
using TaskManager.CommonModels.Repositories;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.UnitOfWork;

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
            services.AddScoped<IRepository<Domain.Entities.Task>, Repository<Domain.Entities.Task>>();
            services.AddScoped<IRepository< Domain.Entities.Column>, Repository< Domain.Entities.Column>>();
            services.AddScoped<IRepository<Domain.Entities.ProjectUser>, Repository<Domain.Entities.ProjectUser>>();
            services.AddScoped<IRepository<Comments>, Repository< Comments>>();

            services.AddScoped<IUnitOfWork<Project>, UnitOfWork<Project>>();
            services.AddScoped<IUnitOfWork<Domain.Entities.Task>, UnitOfWork<Domain.Entities.Task>>();
            services.AddScoped<IUnitOfWork<Domain.Entities.Column>, UnitOfWork<Domain.Entities.Column>>();
            services.AddScoped<IUnitOfWork<Domain.Entities.ProjectUser>, UnitOfWork<Domain.Entities.ProjectUser>>();
            services.AddScoped<IUnitOfWork<Comments>, UnitOfWork<Comments>>();

            services.AddScoped<GenericService<Project>>();
            services.AddScoped<GenericService<Domain.Entities.Column>>();
            services.AddScoped<GenericService<Domain.Entities.Task>>();
            services.AddScoped<GenericService<Domain.Entities.ProjectUser>>();
            services.AddScoped<GenericService<Comments>>();

        }
    }
}
