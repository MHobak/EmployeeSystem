using System.Net;
using FluentValidation.Results;

namespace EmployeeSystem.Application.Exceptions
{
    public class ModelValidationException : Exception, IValidationException
    {
        public ModelValidationException()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ModelValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "One or more validation failures have occurred.";

        public IDictionary<string, string[]> Errors { get; }
    }
}