﻿using EmployeeSystem.Application.Common.Dto;
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
        public async Task<ResponseWrapper<List<EmployeeResponse>>> GetAllAsync(string? nameFilter, int pageNumber, int pageSize)
        {
            ResponseWrapper<List<EmployeeResponse>> response = new();
            if(pageSize > 0 ) response.PageSize = pageSize;
            if(pageNumber > 0 ) response.PageNumber = pageNumber;

            var query = _employeeRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(x => 
                    x.Name.ToLower().Contains(nameFilter.ToLower()) ||
                    x.LastName.ToLower().Contains(nameFilter.ToLower())
                );
            }

            var result = query.OrderBy(x => x.BornDate)
            .Skip((response.PageNumber - 1) * response.PageSize)
            .Take(response.PageSize)
            .ToList();

            response.Data = result.Select(x => CreateResponse(x)).ToList();
            response.TotalItems = query.Count();

            return response;
        }

        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            
            if (employee is null)
            {
                throw new NotFoundException();
            }

            return CreateResponse(employee);
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
                ID = request.ID,
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
