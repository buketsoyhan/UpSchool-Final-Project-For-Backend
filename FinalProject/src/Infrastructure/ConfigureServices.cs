using Application.Common.Interfaces;
using Infrastructure.Persistence.Configurations.Context;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string wwwrootPath)
        {
            var connectionString = configuration.GetConnectionString("MariaDB");

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IApplicationDbContext>(provider=>provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IExcelService, ExcelManager>();

            // Singleton Services
            services.AddSingleton<IEmailService>(new EmailManager(wwwrootPath));

            return services;
        }
    }
}
