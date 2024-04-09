using ir.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.domain.Entities
{
    public class Lead : BaseEntity
    {

        public int CustomerId { get; set; }
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
