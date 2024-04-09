using ir.domain.Entities;

namespace ir.infrastructure.DTOs.OpportunityDtos
{
    public class OpportunityCreateDto
    {
        public string OpportunityName { get; set;}
        public int PotentialRevenue { get; set; }
        public float ClosingProbability { get; set; }
        public DateTime CloseDate { get; set; }
        public OpportunityStatus OpportunityStatus { get; set; }

        public int LeadId { get; set; }
    }
}
