using ir.shared;
using System.ComponentModel.DataAnnotations;

namespace ir.domain.Entities
{
    public class Lead : BaseEntity
    {

        public int CustomerId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "lead name must be between 2 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z_\s] +$", ErrorMessage = "lead name can only contain letters , underscore and spaces.")]
        public string LeadName { get; set; }    
        public Customer Customer { get; set; }

        //public int UserId { get; set; }

        //public User User { get; set; }

        public LeadStatus LeadStatus { get; set; }
        public ICollection<Opportunity> Opportunities { get; set; }
        public Lead()
        {
            CustomerId = default;
        }
    }
}
