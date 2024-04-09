using ir.domain.Entities;

namespace ir.infrastructure.DTOs.LeadDtos
{
    public class UpdateLeadStatusDto
    {
        public int LeadId { get; set; }
        public LeadStatus NewStatus { get; set; }
    }
}