using ir.domain.Entities;
using ir.infrastructure.DTOs.CustomerDtos;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.Repo.Infrastructure
{
    public interface ICustomerService : IGeneralCrudService<Customer,CustomerCreateDto,CustomerGetDto>
    {
        Task<IEnumerable<LeadListDto>> GetLeadsofCustomersByIdAsync(int id);
    }
}
