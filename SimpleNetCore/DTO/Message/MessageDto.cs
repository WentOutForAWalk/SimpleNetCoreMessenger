using System.ComponentModel.DataAnnotations;

namespace SimpleNetCore.DTO.Message;

public record MessageDto
{
    public Guid MessageId { get; init; } = Guid.NewGuid();
    [Required] public string SenderName { get; set; } = string.Empty;
    [Required] public string Text {  get; set; } = string.Empty;
    public DateTime SentAt {  get; init; } = DateTime.UtcNow;
}

