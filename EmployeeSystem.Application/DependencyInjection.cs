using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Application.Services;
using EmployeeSystem.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddTransient<IValidator<EmployeeRequest>, EmployeeValidator>();

            services.AddAutoMapper(typeof(DependencyInjection));

            return services;
        }
    }
}
