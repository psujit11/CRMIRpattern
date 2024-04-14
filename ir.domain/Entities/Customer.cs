using ir.shared;
using System.ComponentModel.DataAnnotations;

namespace ir.domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Customer name can only contain letters and spaces.")]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MobileNumber]
        public string MobileNumber { get; set; }
        [Required]
        public string Company { get; set; }
        public string Department { get; set; }
        public ICollection<Lead> Leads { get; set; }
    }
}