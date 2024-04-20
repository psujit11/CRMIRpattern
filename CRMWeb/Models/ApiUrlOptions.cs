using System.ComponentModel.DataAnnotations;

namespace CRMWeb.Models
{
    public class ApiUrlOptions
    {
        [Required]
        public string RegisterUrl { get; set; } = default!;
        [Required]
        public string LoginUrl { get; set; } = default!;
    }
}
