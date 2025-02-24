namespace AppMeteo.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public required Measure Measure { get; set; }
        public String AlertMessage { get; set; } = string.Empty;
    }
}