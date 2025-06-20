﻿using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBoard.DataBase.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetDatabase(
       this IServiceCollection services,
       string connectionString)
        {
            // Configurar DbContext
            services.AddDbContextPool<DataBaseContext>(options =>
                options.UseNpgsql(connectionString));

            var repositories = typeof(ServiceCollectionExtensions).Assembly.DefinedTypes.Where(x => x.FullName!.EndsWith("Repository"));


            foreach (var type in repositories)
            {
                var interfaceType = type.GetInterface($"I{type.Name}");
                if (interfaceType == null) { continue; }
                services.AddScoped(interfaceType, type);
            }

            // Registrar repositórios
            //services.AddScoped<IMessageRepository, MessageRepository>();
            //services.AddScoped<IGroupRepository, GroupRepository>();

            // Registrar UnitOfWork se estiver usando
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
