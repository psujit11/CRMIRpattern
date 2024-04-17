using ir.domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ir.Persistance
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        
       
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.ApplicationUser)
                .WithMany()
                .HasForeignKey(l => l.ApplicationUserId);
        }*/
    }
}
