using FluentValidation;
using ir.domain.Entities;

namespace ir.infrastructure.Validation
{
    public class LeadValidator : AbstractValidator<Lead>
    {
        public LeadValidator()
        {
            RuleFor(x => x.LeadName)
           .NotEmpty().WithMessage("Lead name is required.")
           .Length(2, 100).WithMessage("Lead name must be between 2 and 100 characters long.")
           .Matches(@"^[a-zA-Z_\s]+$").WithMessage("Lead name can only contain letters, underscore, and spaces.");
        }
    }
}
