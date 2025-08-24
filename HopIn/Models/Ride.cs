using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopIn.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }

        [Required]
        public string DriverId { get; set; } = string.Empty;

        [ForeignKey("DriverId")]
        public ApplicationUser Driver { get; set; }

        [Required]
        public string StartLocation { get; set; } = string.Empty;

        [Required]
        public string Destination { get; set; } = string.Empty;

        public DateTime RideDate { get; set; }

        public int AvailableSeats { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerSeat { get; set; }

        // Navigation
        public ICollection<RideRequest>? Requests { get; set; }
    }
}
