using ir.domain.Entities;

namespace ir.infrastructure.DTOs.LeadDtos
{
    public class LeadGetDto
    {
        public int Id { get; set; }
        public string LeadName { get; set; }
        public int CustomerId { get; set; }
        
        

        public string ApplicationUserId { get; set; }

        public LeadStatus LeadStatus { get; set; }
        //public ICollection<Opportunity> Opportunities { get; set; }
    }
}
