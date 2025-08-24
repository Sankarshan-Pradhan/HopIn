using HopIn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HopIn.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ride> Rides { get; set; }
        public DbSet<RideRequest> RideRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Prevent multiple cascade paths
            builder.Entity<RideRequest>()
                .HasOne(rr => rr.Ride)
                .WithMany(r => r.Requests)
                .HasForeignKey(rr => rr.RideId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RideRequest>()
                .HasOne(rr => rr.Passenger)
                .WithMany(u => u.RideRequests)
                .HasForeignKey(rr => rr.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
