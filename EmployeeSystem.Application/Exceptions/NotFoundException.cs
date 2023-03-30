using System.Net;

namespace EmployeeSystem.Application.Exceptions
{
    public class NotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "The record wasn't found.";
    }
}