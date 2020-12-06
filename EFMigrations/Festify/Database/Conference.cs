using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Festify.Database
{
    public class Conference
    {
        public int ConferenceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Identifier { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}