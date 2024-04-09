using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;

namespace ir.infrastructure.Repo.Infrastructure
{
    public interface ILeadService : IGeneralCrudService<Lead,LeadCreateDto,LeadGetDto>
    {
        //Task AssignToSalesRepresentative(AssignSalesRepresentativeDto dto);
        Task<IEnumerable<OpportunityListDto>> GetLeadWithOpportunitiesByIdAsync(int id);
        Task UpdateLeadStatus(UpdateLeadStatusDto dto);
    }
}
 