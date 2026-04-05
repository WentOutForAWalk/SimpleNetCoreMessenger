namespace Backend.API.Models
{
    public class Message
    {
        public string OwnerId { get; set; } = string.Empty;
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public string SenderName { get; set; } = string.Empty;
        public string? Text { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Guid ChannelId { get; set; }
    }
}
