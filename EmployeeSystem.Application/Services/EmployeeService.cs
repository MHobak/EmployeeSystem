using AutoMapper;
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
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IValidator<EmployeeRequest> employeeValidator, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _mapper = mapper;
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

            response.Data = _mapper.Map<List<EmployeeResponse>>(result);
            response.TotalItems = query.Count();

            return response;
        }

        /// <summary>
        /// Gets one record by its id
        /// </summary>
        /// <param name="id">Identificator</param>
        /// <returns>Found employee</returns>
        /// <exception cref="NotFoundException">Exception in case the employee is not found</exception>
        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            
            if (employee is null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<EmployeeResponse>(employee);
        }

        /// <summary>
        /// Persist a new employee record
        /// </summary>
        /// <param name="request">Employye record to create</param>
        /// <returns>New Created employee</returns>
        /// <exception cref="DuplicateRfcException">Exception in case the employee is not found</exception>
        /// <exception cref="ModelValidationException">Exception in case the employee is not found</exception>
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

            var newEmployee = _mapper.Map<Employee>(request);
            newEmployee.ID = 0; //prevent errors

            await _employeeRepository.AddAsync(newEmployee);
            return _mapper.Map<EmployeeResponse>(newEmployee);
        }

        /// <summary>
        /// Update an existing employye
        /// </summary>
        /// <param name="request">employee record wiwth new data</param>
        /// <returns>Updated employee record</returns>
        /// <exception cref="NotFoundException">Exception in case the employee is not found</exception>
        /// <exception cref="ModelValidationException">Exception in case the employee data is not valid</exception>
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

            Employee newEmployee = _mapper.Map<Employee>(request);

            await _employeeRepository.UpdateAsync(newEmployee);
            return _mapper.Map<EmployeeResponse>(newEmployee);
        }

        /// <summary>
        /// Deletes an employee record bby its id
        /// </summary>
        /// <param name="id">Employee identificator</param>
        /// <returns>Completed Task</returns>
        /// <exception cref="NotFoundException">Exception in case the employee is not found</exception>
        public async Task DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException();
            }

            await _employeeRepository.DeleteAsync(employee);
        }
    }
}
