using EmployeeSystem.Application.Common.Dto;

namespace EmployeeSystem.Application.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponse>> GetAll();
        Task<EmployeeResponse> Create(EmployeeRequest request);
    }
}