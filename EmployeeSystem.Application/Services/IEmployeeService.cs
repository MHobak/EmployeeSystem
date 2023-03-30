using EmployeeSystem.Application.Common.Dto;

namespace EmployeeSystem.Application.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponse>> GetAllAsync();
        Task<EmployeeResponse> GetByIdAsync(int id);
        Task<EmployeeResponse> CreateAsync(EmployeeRequest request);
        Task<EmployeeResponse> UpdateAsync(EmployeeRequest request);
        Task DeleteAsync(int id);
    }
}