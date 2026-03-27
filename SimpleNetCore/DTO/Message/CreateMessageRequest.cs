using System.ComponentModel.DataAnnotations;

namespace SimpleNetCore.DTO.Message;


public record CreateMessageRequest(
    [Required] string SenderName,
    [Required] string Text
);
