using System;
using System.ComponentModel.DataAnnotations;

namespace Festify.Database
{
    public class Session
    {
        public int SessionId { get; set; }
        public Guid SessionGuid { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Speaker { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }
}