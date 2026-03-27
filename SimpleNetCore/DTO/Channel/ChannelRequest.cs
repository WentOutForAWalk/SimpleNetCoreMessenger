using System.ComponentModel.DataAnnotations;

namespace SimpleNetCore.DTO.Channel;

public record ChannelRequest(
    [Required][StringLength(22)] string ChannelName
);
