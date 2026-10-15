using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RaceDay.Api.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Upcoming";

        public string OrganiserId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganiserId))]
        public AppUser? Organiser { get; set; }

        public ICollection<Race> Races { get; set; } = new List<Race>();
    }
}
