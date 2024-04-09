using ir.infrastructure.Repo.Infrastructure;
using ir.infrastructure.Repo.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace ir.infrastructure
{
    public static class InfrastructureServiceRegistration
    {
       public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            return services;
        }
    }
    
}
