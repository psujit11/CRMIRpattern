using ir.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.DTOs.LeadDtos
{
    public class LeadCreateDto
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "lead name must be between 2 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z_\s] +$", ErrorMessage = "lead name can only contain letters , underscore and spaces.")]
        public string LeadName { get; set; }
        /*public int UserId { get; set; }

        public User User { get; set; }*/

        public LeadStatus LeadStatus { get; set; }

    }
}
