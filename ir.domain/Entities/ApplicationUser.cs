using Microsoft.AspNetCore.Identity;

namespace ir.domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
}
}
