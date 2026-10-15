using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        public int RaceId { get; set; }

        [ForeignKey(nameof(RaceId))]
        public Race? Race { get; set; }

        public int ParticipantId { get; set; }

        [ForeignKey(nameof(ParticipantId))]
        public Participant? Participant { get; set; }

        public TimeSpan? FinishTime { get; set; }

        public int? Position { get; set; }
    }
}