using ir.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ir.infrastructure.DTOs.OpportunityDtos
{
    public class OpportunityCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Opportunity name must be between 2 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z_\s] +$", ErrorMessage = "Opportunity name can only contain letters , underscore and spaces.")]

        public string OpportunityName { get; set;}
        public int PotentialRevenue { get; set; }
        [Range(0, 100, ErrorMessage = "Closing probability must be between 0 and 100.")]
        public float ClosingProbability { get; set; }
        public DateTime CloseDate { get; set; }
        public OpportunityStatus OpportunityStatus { get; set; }

        public int LeadId { get; set; }
    }
}
