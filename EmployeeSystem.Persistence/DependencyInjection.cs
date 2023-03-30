using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
