using SimpleNetCore.DTO.Message;
using System.ComponentModel.DataAnnotations;
namespace SimpleNetCore.DTO.Channel;

public record ChannelDto
{
    public Guid ChannelId { get; init; } = Guid.NewGuid();
    [Required][StringLength(22)] public string ChannelName { get; set; } = string.Empty;
    public List<MessageDto> Messages { get; init; } = new();

}
