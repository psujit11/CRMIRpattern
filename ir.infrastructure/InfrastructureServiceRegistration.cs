using FluentValidation;
using ir.infrastructure.Repo.Infrastructure;
using ir.infrastructure.Repo.Services;
using ir.infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace ir.infrastructure
{
    public static class InfrastructureServiceRegistration
    {
       public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
            services.AddValidatorsFromAssemblyContaining<LeadValidator>();
            services.AddValidatorsFromAssemblyContaining<OpportunityValidator>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
    
}
