using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.ChatService
{
    public static class BusinessChatServiceRegistration
    {
        public static void BusinessChatServices(this IServiceCollection services)
        {
     

           // services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
