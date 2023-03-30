using System.Net;

namespace EmployeeSystem.Application.Exceptions
{
    public class DuplicateRfcException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "This RFC is already in use";
    }
}