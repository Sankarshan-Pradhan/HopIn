using Microsoft.AspNetCore.Identity;

namespace HopIn.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public bool IsDriver { get; set; }

        // Navigation properties
        public ICollection<Ride>? Rides { get; set; }      // As a driver
        public ICollection<RideRequest>? RideRequests { get; set; } // As a passenger
    }
}
