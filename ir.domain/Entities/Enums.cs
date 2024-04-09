using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.domain.Entities
{
    public enum LeadStatus
    {
        New,
        Contacted,
        Qualified,
        Converted,
        LeadLost
    }

    public enum OpportunityStatus
    {
        Prospecting,
        Qualification,
        Proposal,
        Negotiation
    }
}
