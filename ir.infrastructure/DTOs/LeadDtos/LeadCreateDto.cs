using ir.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.DTOs.LeadDtos
{
    public class LeadCreateDto
    {
        public int CustomerId { get; set; }
        public string LeadName { get; set; }
        /*public int UserId { get; set; }

        public User User { get; set; }*/

        public LeadStatus LeadStatus { get; set; }

    }
}
