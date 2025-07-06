using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Pass the options to the base class constructor
        {
        }
       
        public DbSet<AppUser> AppUsers { get; set; } // DbSet for AppUser entity
    }
}
