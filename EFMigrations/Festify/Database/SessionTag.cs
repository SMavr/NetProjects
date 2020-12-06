namespace Festify.Database
{
    public class SessionTag
    {
        public int SessionId { get; set; }

        public Session Session { get; set; }

        public string Tag { get; set; }
    }
}
