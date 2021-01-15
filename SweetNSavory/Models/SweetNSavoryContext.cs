using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SweetNSavory.Models
{
  public class SweetNSavoryContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flavors {get; set;}
    public DbSet<Treat> Treats {get; set;}
    public DbSet<FlavorTreat> FlavorTreat {get; set;}

    public SweetNSavoryContext(DbContextOptions options) : base(options){ }
  }
}