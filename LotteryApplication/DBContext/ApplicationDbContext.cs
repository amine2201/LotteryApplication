using LotteryApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace LotteryApplication.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            To create a one-to - one relationship between the ApplicationUser and Participation entities, you can use Entity Framework's fluent API in the OnModelCreating method of your Entity Framework DbContext class. Here is an example of how you can do this:

Copy code
public class MyDbContext : DbContext
        {
            public DbSet<ApplicationUser> ApplicationUsers { get; set; }
            public DbSet<Participation> Participations { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Participation>()
                    .HasOne(p => p.Participant)
                    .WithOne(a => a.Participation)
                    .HasForeignKey<Participation>(p => p.ParticipantId);
            }

        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<IdentityRole> roleUsers { get; set; }
        public DbSet<Participation> participations { get; set; }

    }
}
