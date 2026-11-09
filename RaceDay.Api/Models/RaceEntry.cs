using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    public class RaceEntry
    {
        public int RaceEntryId { get; set; }

        public int RaceId { get; set; }
        [ForeignKey(nameof(RaceId))]
        public Race? Race { get; set; }

        // Changed to string to match ASP.NET Identity User ID
        public string ParticipantId { get; set; } = string.Empty;

        // --- NEW DETAILS FIELDS ---
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string EmergencyContactName { get; set; } = string.Empty;

        [Required]
        public string EmergencyContactPhone { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";
    }
}