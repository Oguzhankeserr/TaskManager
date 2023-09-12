using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.Business.Infrastructure.UnitOfWork;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.Abstractions.Storage;
using TaskManager.Business.Infrastructure.Services.Storage;
using TaskManager.Business.Infrastructure.Services.Storage.Local;
using TaskManager.Business.Infrastructure.Enums;
using TaskManager.Business.Infrastructure.Services.Storage.Azure;
using Microsoft.Extensions.Options;
using TaskManager.Business.LogService.Context;

namespace TaskManager.Business.Infrastructure
{
    public static class InfrastructureBusinessServiceRegistration
    {
        public static void AddBusinessInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("TaskManagerBusinessConnection"))
                       .UseLowerCaseNamingConvention();
            }, ServiceLifetime.Scoped);


            services.AddDbContext<LogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("TaskManagerLogConnection")).UseLowerCaseNamingConvention();
            });


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddScoped<IStorageService, StorageService>();

            
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        //public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType) where T : Storage, IStorage //Burayı kaldırabilirim daha sonra bakacağım
        //{
        //    switch (storageType)
        //    {
        //        case StorageType.Local:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //        case StorageType.Azure:
        //            serviceCollection.AddScoped<IStorage, AzureStorage>();
        //            break;
        //        case StorageType.AWS:
        //            break;
        //        default:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //    }
        //}

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<BusinessDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
