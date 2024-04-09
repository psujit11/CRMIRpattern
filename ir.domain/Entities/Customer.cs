using ir.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ir.domain.Entities
{
    public class Customer : BaseEntity
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public double MobileNumber { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public ICollection<Lead> Leads { get; set; }
    }
}