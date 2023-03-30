using EmployeeSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
