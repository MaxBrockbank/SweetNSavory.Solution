using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SweetNSavory.Models
{
  public class SweetNSavoryContext : IdentiyDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flavors {get; set;}
    public DbSet<Treat> Treats {get; set;}
    public DbSet<FlavorTreat> FlavorTreat {get; set;}

    public SweetNSavoryContext(DbContextOptions option) : base(options){ }
  }
}