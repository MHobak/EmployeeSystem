using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Application.Common.Interface.Persistence;
using EmployeeSystem.Application.Exceptions;
using EmployeeSystem.Domain.Entities;
using FluentValidation;

namespace EmployeeSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<EmployeeRequest>  _employeeValidator;

        public EmployeeService(IEmployeeRepository employeeRepository, IValidator<EmployeeRequest>  employeeValidator)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
        }

        public Task<List<EmployeeResponse>> GetAllAsync()
        {
            var list = _employeeRepository.GetAll()
                .Select(x => CreateResponse(x)).ToList(); //change by ToListAsync

            return Task.FromResult(list);
        }

        public Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var employee = _employeeRepository.GetAll()
                .Where(x => x.ID == id )
                .Select(x => CreateResponse(x)).FirstOrDefault(); //change by ToListAsync
            
            if (employee is null)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(employee);
        }

        public async Task<EmployeeResponse> CreateAsync(EmployeeRequest request)
        {
            var employee = await _employeeRepository.GetByRFCAsync(request.RFC);

            if (employee is not null)
            {
                throw new DuplicateRfcException();
            }

            var validationResult = await _employeeValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ModelValidationException(validationResult.Errors);
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

        public async Task<EmployeeResponse> UpdateAsync(EmployeeRequest request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.ID);

            if (employee is null)
            {
                throw new NotFoundException();
            }

            var validationResult = await _employeeValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ModelValidationException(validationResult.Errors);
            }

            Employee newEmployee = CreateModel(request);

            await _employeeRepository.UpdateAsync(newEmployee);
            return CreateResponse(newEmployee);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException();
            }

            await _employeeRepository.DeleteAsync(employee);
        }

        private static Employee CreateModel(EmployeeRequest request)
        {
            return new Employee
            {
                Name = request.Name,
                LastName = request.LastName,
                RFC = request.RFC,
                BornDate = request.BornDate,
                Status = request.Status
            };
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
