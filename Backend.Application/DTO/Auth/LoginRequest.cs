using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO.Auth;

public record LoginRequest(
    [Required] string Name,
    [Required] string Password
);
