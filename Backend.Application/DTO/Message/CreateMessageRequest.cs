using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO.Message;


public record CreateMessageRequest(
    [Required] string Text
);
