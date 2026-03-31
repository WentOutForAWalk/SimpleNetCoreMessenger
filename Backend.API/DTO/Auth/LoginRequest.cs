using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTO.Auth;

public record LoginRequest(
    [Required] string Name,
    [Required] string Password
);
