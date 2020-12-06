using System.ComponentModel.DataAnnotations;

namespace Festify.Database
{
    public class SessionTag
    {
        public int SessionId { get; set; }

        public Session Session { get; set; }

        [MaxLength(10)]
        public string Tag { get; set; }
    }
}
