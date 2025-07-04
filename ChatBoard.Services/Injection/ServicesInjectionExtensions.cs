using Microsoft.Extensions.DependencyInjection;

namespace ChatBoard.Services.Injection
{
    public static class ServicesInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            var appServices = typeof(ServicesInjectionExtensions).Assembly.DefinedTypes.Where(x => x.FullName!.EndsWith("Service"));

            foreach (var type in appServices)
            {
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    services.AddScoped(inter, type);
                }
            }

            return services;
        }
    }
}
