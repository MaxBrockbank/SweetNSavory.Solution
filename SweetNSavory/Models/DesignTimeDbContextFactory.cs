using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SweetNSavory.Models
{
  public class SweetNSavoryContextFactory : IDesignTimeDbContextFactory<SweetNSavoryContext>
  {

    SweetNSavoryContext IDesignTimeDbContextFactory<SweetNSavoryContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<SweetNSavoryContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new SweetNSavoryContext(builder.Options);
    }
  }
}