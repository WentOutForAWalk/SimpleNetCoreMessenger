using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTO.Message;


public record CreateMessageRequest(
    [Required] string Text
);
