using System.Net;

namespace EmployeeSystem.Application.Exceptions
{
    public interface IValidationException: IServiceException
    {
        public IDictionary<string, string[]> Errors { get; }
    }
}