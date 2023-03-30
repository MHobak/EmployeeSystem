using AutoMapper;
using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Mappers
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeRequest, Employee>();

            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest =>
                dest.EmployeeStatus,
                opt => opt.Ignore()
            );
        }
    }
}
