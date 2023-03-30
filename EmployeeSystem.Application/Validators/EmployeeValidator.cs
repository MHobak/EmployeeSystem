using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Application.Common.Interface.Persistence;
using FluentValidation;

namespace EmployeeSystem.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1,200);
            
            RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(1,200);

            RuleFor(x => x.BornDate)
            .Must(BeAValidDate);

            RuleFor(x => x.RFC)
            .NotEmpty()
            .Matches(@"^(?:[A-Z&Ñ]{3}|[A-Z][AEIOU][A-Z]{2})\d{6}[A-Z0-9]{3}$") //Regex pattern to match the correct format of a RFC
            .WithMessage("Invalid RFC format");
            //.Length(12,13); //rfc is usually between 12 and 13 char long

        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}