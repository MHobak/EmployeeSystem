using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employeeList = new List<Employee>();

        /// <summary>
        /// Method to get the employee
        /// </summary>
        /// <returns>Table of employees as IQueryable</returns>
        public IQueryable<Employee> GetAll()
        {
            return _employeeList.AsQueryable();
        }

        /// <summary>
        /// Method to get the employee by the RFC
        /// </summary>
        /// <param name="rfc">RFC code string</param>
        /// <returns>Employee Object</returns>
        public Task<Employee?> GetByRFCAsync(string rfc)
        {
            var employee = _employeeList.FirstOrDefault(x => x.RFC == rfc);
            
            return Task.FromResult(employee);
        }

        /// <summary>
        /// Method to get the employee by its identifier Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Employee Object</returns>
        public Task<Employee?> GetByIdAsync(int id)
        {
            return Task.FromResult(_employeeList.FirstOrDefault());
        }

        /// <summary>
        /// Method to persist an employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Created employee</returns>
        public Task<Employee> AddAsync(Employee employee)
        {
            employee.ID = _employeeList.Count + 1;
            _employeeList.Add(employee);
            
            return Task.FromResult(employee);
        }

        /// <summary>
        /// Method to update the employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Updated employee object</returns>
        public Task<Employee> UpdateAsync(Employee employee)
        {
            var updatedEmployee = _employeeList.FirstOrDefault(x => x.ID == employee.ID);
            updatedEmployee = employee;
            return Task.FromResult(employee);
        }

        /// <summary>
        /// Method to delete an employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Task</returns>
        public Task DeleteAsync(Employee employee)
        {
            _employeeList.Remove(employee);

            return Task.CompletedTask;
        }
    }
}
