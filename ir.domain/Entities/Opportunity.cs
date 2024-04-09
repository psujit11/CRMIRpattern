using ir.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.domain.Entities
{
    public class Opportunity : BaseEntity
    {   
        public string OpportunityName { get; set; }

        public int LeadId { get; set; }

        public Lead Lead { get; set; }
        public int PotentialRevenue { get; set; }
        public float ClosingProbability { get; set; }
        public DateTime CloseDate { get; set; }
        public OpportunityStatus OpportunityStatus { get; set; }

    }
}
