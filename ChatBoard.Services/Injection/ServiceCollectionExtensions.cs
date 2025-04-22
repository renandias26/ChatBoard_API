using ChatBoard.Services.Interface;
using ChatBoard.Services.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBoard.Services.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            var appServices = typeof(ServiceCollectionExtensions).Assembly.DefinedTypes.Where(x => x.FullName!.EndsWith("Service"));

            foreach (var type in appServices)
            {
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    services.AddScoped(inter, type);
                }
            }

            //services.AddScoped<IMessageService, MessageService>();
            //services.AddScoped<IGroupService, GroupService>();

            return services;
        }
    }
}
