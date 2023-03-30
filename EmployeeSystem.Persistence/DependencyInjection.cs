using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Persistence;
using EmployeeSystem.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string? connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<EmployeeDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
