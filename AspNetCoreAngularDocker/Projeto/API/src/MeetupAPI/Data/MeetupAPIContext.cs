using MeetupAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Data
{
    public class MeetupAPIContext : DbContext
    {
        public MeetupAPIContext (DbContextOptions<MeetupAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meetup>().HasKey(m => m.Id);
            modelBuilder.Entity<Meetup>().Property(m => m.Nome).HasColumnType("varchar(100)");
            modelBuilder.Entity<Meetup>().Property(m => m.Descricao).HasColumnType("varchar(1000)");
            modelBuilder.Entity<Meetup>().Property(m => m.Local).HasColumnType("varchar(150)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Meetup> Meetup { get; set; }
    }
}
