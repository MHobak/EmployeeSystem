using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employeeList = new List<Employee>();

        public EmployeeRepository()
        {      
            _employeeList.Add(new Employee{
                ID = 1,
                Name = "Alberto ",
                LastName = "Cruz ",
                BornDate = new DateTime(1998,06,18),
                RFC = "CUGA980618N4A",
                Status = Domain.Enums.EmployeeStatus.Inactive});

                _employeeList.Add(new Employee{
                ID = 2,
                Name = "Roberto",
                LastName = "González",
                BornDate = new DateTime(1989,03,09),
                RFC = "GOCR980618VE3",
                Status = Domain.Enums.EmployeeStatus.Inactive});

                _employeeList.Add(new Employee{
                ID = 3,
                Name = "Alicia",
                LastName = "Guerrero",
                BornDate = new DateTime(1997,02,14),
                RFC = "GUPA980618CK5",
                Status = Domain.Enums.EmployeeStatus.Inactive});

                _employeeList.Add(new Employee{
                ID = 4,
                Name = "Juan Manuel",
                LastName = "Cruz",
                BornDate = new DateTime(2001,11,30),
                RFC = "CUPJ980618T9A",
                Status = Domain.Enums.EmployeeStatus.Inactive});

                _employeeList.Add(new Employee{
                ID = 5,
                Name = "María de Jesús",
                LastName = "Méndez",
                BornDate = new DateTime(1999,12,02),
                RFC = "MEOJ980618JT0",
                Status = Domain.Enums.EmployeeStatus.Inactive});

                _employeeList.Add(new Employee{
                ID = 6,
                Name = "Melanie",
                LastName = "Flores",
                BornDate = new DateTime(1991,05,21),
                RFC = "FOTM980618PI9",
                Status = Domain.Enums.EmployeeStatus.Inactive});
        }

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
