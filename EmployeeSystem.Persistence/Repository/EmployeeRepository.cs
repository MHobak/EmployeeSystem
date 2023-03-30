using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employeeList = new List<Employee>();

        public Task<Employee> AddAsync(Employee employee)
        {
            employee.ID = _employeeList.Count + 1;
            _employeeList.Add(employee);
            
            return Task.FromResult(employee);
        }

        public Task DeleteAsync(Employee employee)
        {
            _employeeList.Remove(employee);

            return Task.CompletedTask;
        }

        public IQueryable<Employee> GetAll()
        {
            return _employeeList.AsQueryable();
        }

        public Task<Employee?> GetByRFCAsync(string rfc)
        {
            var employee = _employeeList.FirstOrDefault(x => x.RFC == rfc);
            
            return Task.FromResult(employee);
        }
    }
}
