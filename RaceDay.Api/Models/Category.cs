using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RaceDay.Api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public ICollection<Race> Races { get; set; } = new List<Race>();
    }
}
