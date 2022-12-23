using LotteryApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LotteryApplication.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participation>()
                .HasOne(p => p.Participant)
                .WithOne(a => a.Participation)
                .HasForeignKey<Participation>(p => p.ParticipantId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<IdentityRole> roleUsers { get; set; }
        public DbSet<Participation> participations { get; set; }

    }
}
