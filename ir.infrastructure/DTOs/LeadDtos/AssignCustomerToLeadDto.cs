using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.infrastructure.DTOs.LeadDtos
{
    public class AssignCustomerToLeadDto
    {
        public int LeadId { get; set; }
        public int CustomerId { get; set; }
    }

}
