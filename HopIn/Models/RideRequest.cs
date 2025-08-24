using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopIn.Models
{
    public class RideRequest
    {
        [Key]
        public int RequestId { get; set; }

        public int RideId { get; set; }
        [ForeignKey("RideId")]
        public Ride Ride { get; set; }

        public string PassengerId { get; set; } = string.Empty;
        [ForeignKey("PassengerId")]
        public ApplicationUser Passenger { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string? Message { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AgreedCost { get; set; }
    }
}
