using System.Net;

namespace EmployeeSystem.Application.Exceptions
{
    public interface IServiceException
    {
        HttpStatusCode StatusCode {get; }         
        string ErrorMessage { get; }
    }
}