using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext ?? throw new ArgumentNullException(nameof(employeeDbContext));
        }

        /// <summary>
        /// Method to get the employee
        /// </summary>
        /// <returns>Table of employees as IQueryable</returns>
        public IQueryable<Employee> GetAll()
        {
            return _employeeDbContext.Employees.AsQueryable();
        }

        /// <summary>
        /// Method to get the employee by the RFC
        /// </summary>
        /// <param name="rfc">RFC code string</param>
        /// <returns>Employee Object</returns>
        public async Task<Employee?> GetByRFCAsync(string rfc)
        {     
            return await _employeeDbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.RFC == rfc);
        }

        /// <summary>
        /// Method to get the employee by its identifier Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Employee Object</returns>
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeDbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        }

        /// <summary>
        /// Method to persist an employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Created employee</returns>
        public async Task<Employee> AddAsync(Employee employee)
        {
            await _employeeDbContext.Employees.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();

            return employee;
        }

        /// <summary>
        /// Method to update the employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Updated employee object</returns>
        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _employeeDbContext.Employees.Update(employee);
            await _employeeDbContext.SaveChangesAsync();

            return employee;
        }

        /// <summary>
        /// Method to delete an employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Task</returns>
        public async Task DeleteAsync(Employee employee)
        {
            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
