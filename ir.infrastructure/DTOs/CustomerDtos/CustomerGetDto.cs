using ir.domain.Entities;

namespace ir.infrastructure.DTOs.CustomerDtos
{
    public class CustomerGetDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public double MobileNumber { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        //public ICollection<Lead> Leads { get; set; }
    }
}
