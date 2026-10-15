using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }

        [Required]
        [StringLength(100)]
        public string RaceName { get; set; } = string.Empty;

        // 👇 Add [Precision] attribute here
        [Precision(10, 2)]
        public decimal DistanceKm { get; set; }

        public DateTime ScheduledTime { get; set; }

        // ... rest of your properties
    }
}