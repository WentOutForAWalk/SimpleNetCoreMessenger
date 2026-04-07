using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO.Channel;

public record ChannelRequest(
    [Required][StringLength(22)] string ChannelName
);
