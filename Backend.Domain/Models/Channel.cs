namespace Backend.Domain.Models;
public class Channel
{
    public Guid ChannelId { get; init; } = Guid.NewGuid();
    public string ChannelName { get; set; } = string.Empty;
    public List<Message> Messages { get; init; } = new();
    public string OwnerId { get; set; } = string.Empty;
}