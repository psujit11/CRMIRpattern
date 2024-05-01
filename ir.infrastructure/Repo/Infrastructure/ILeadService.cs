using ir.domain.Entities;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using Microsoft.AspNetCore.Mvc;

namespace ir.infrastructure.Repo.Infrastructure
{
    public interface ILeadService : IGeneralCrudService<Lead,LeadCreateDto,LeadGetDto>
    {
        Task AssignLeadToUserByEmailAsync(int leadId, AssignLeadDto assignLeadDto);
        Task AssignCustomerToLeadAsync(AssignCustomerToLeadDto dto);
        Task<IEnumerable<OpportunityListDto>> GetLeadWithOpportunitiesByIdAsync(int id);
        Task UpdateLeadStatus(UpdateLeadStatusDto dto);
        Task<IEnumerable<LeadGetDtoWithOpportunities>> GetAllLeadsWithOpportunitiesAsync();
    }
}
 