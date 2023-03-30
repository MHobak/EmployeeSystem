using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Common.Interface.Persistence
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();
        Task<Employee?> GetByRFCAsync(string rfc);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
