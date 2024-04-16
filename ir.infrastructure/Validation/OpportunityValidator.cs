using FluentValidation;
using ir.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.Validation
{
    public class OpportunityValidator : AbstractValidator<Opportunity>
    {
        public OpportunityValidator()
        {
            RuleFor(x => x.OpportunityName)
           .NotEmpty().WithMessage("Opportunityname is required.")
           .Length(2, 100).WithMessage("Opportunity name must be between 2 and 100 characters long.")
           .Matches(@"^[a-zA-Z_\s]+$").WithMessage("Opportunity name can only contain letters, underscore, and spaces.");
            RuleFor(x => x.PotentialRevenue)
                .NotEmpty().WithMessage("Potential Reveneue is required")
                .InclusiveBetween(0, 100).WithMessage("The probabilty needs to be in range of 0-100");
            RuleFor(x => x.CloseDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("The date cannot be in the past.");
        }
    }
}
