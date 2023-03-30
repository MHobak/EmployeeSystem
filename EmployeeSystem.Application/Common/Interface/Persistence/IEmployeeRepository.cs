using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Common.Interface.Persistence
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();
        Task<Employee?> GetByRFCAsync(string rfc);
        Task<Employee> AddAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
