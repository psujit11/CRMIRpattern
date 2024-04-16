using FluentValidation;
using ir.domain.Entities;
using Ir.Persistance;
using Microsoft.EntityFrameworkCore;
namespace ir.infrastructure.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly AppDbContext _context;
        public CustomerValidator(AppDbContext appDbContext)
        {
            _context = appDbContext;
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer name is required.")
                                       .Length(2, 100).WithMessage("Customer name must be between 2 and 100 characters long.")
                                       .Matches(@"^[a-zA-Z\s]+$").WithMessage("Customer name can only contain letters and spaces.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                  .EmailAddress().WithMessage("Email is not valid.")
                                  .MustAsync(EmailMustNotExist).WithMessage("Email already exists"); 
            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("Mobile number is required.")
                                       .Matches(@"^\d{10}$").WithMessage("Mobile number must be exactly 10 digits.")
                                       .MustAsync(MobileMustNotExist).WithMessage("Mobile number already exist already exists");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required.");
        }
        public async Task<bool> EmailMustNotExist(Customer customer, string email, CancellationToken cancellationToken)
        {
            return !await _context.Customers.AnyAsync(x => x.Email == email, cancellationToken);
        }
        public async Task<bool> MobileMustNotExist(Customer customer, string mobile, CancellationToken cancellationToken)
        {
            return !await _context.Customers.AnyAsync(x => x.MobileNumber == mobile, cancellationToken);
        }
    }
}

