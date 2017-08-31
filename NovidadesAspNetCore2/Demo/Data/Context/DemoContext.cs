using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
  public class DemoContext : DbContext
  {
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Cliente>().HasIndex(c => c.Id);

      base.OnModelCreating(modelBuilder);
    }
  }
}