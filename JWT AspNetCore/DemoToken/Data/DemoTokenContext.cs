using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoToken.Models
{
    public class DemoTokenContext : IdentityDbContext
    {
        public DemoTokenContext (DbContextOptions<DemoTokenContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
    }
}
