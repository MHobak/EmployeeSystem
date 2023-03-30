using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeResponse> Create(EmployeeRequest request)
        {
            var employeee = _employeeRepository.GetByRFCAsync(request.RFC);

            if (employeee == null)
            {
                throw new Exception("RFC already exist.");
            }

            var newEmployee = new Employee
            {
                Name = request.Name,
                LastName = request.LastName,
                RFC = request.RFC,
                BornDate = request.BornDate,
                Status = request.Status
            };

            await _employeeRepository.AddAsync(newEmployee);
            return CreateResponse(newEmployee);
        }


        public Task<List<EmployeeResponse>> GetAll()
        {
            var list = _employeeRepository.GetAll()
                .Select(x => CreateResponse(x)).ToList(); //change by ToListAsync

            return Task.FromResult(list);
        }

        private static EmployeeResponse CreateResponse(Employee newEmployee)
        {
            return new EmployeeResponse
            {
                ID = newEmployee.ID,
                Name = newEmployee.Name,
                LastName = newEmployee.LastName,
                RFC = newEmployee.RFC,
                BornDate = newEmployee.BornDate,
                Status = newEmployee.Status
            };
        }
    }
}
