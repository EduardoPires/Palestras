using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeusFilme.API.Models
{
    public class DemoTokenContext : IdentityDbContext
    {
        public DemoTokenContext(DbContextOptions<DemoTokenContext> options)
            : base(options)
        {
        }

        public DbSet<Filme> Filme { get; set; }
    }
}