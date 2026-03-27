namespace SimpleNetCore.Models
{
    public class Message
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public required string SenderName { get; set; }
        public required string Text { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

    }
}
