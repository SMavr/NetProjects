using System.ComponentModel.DataAnnotations;

namespace Festify.Database
{
    public enum ReachId
    {
        Keynote = 1,
        Breakout = 2,
        OpenSpace = 3
    }

    public class Reach
    {

        public int ReachId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }
    }
}
