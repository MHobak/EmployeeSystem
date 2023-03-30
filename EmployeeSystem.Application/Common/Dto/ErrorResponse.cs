namespace EmployeeSystem.Application.Common.Dto
{
    public record ErrorResponse(
        string errorMessage,
        IDictionary<string, string[]>? errors = null
    );
}