using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTO.Channel;

public record ChannelRequest(
    [Required][StringLength(22)] string ChannelName
);
