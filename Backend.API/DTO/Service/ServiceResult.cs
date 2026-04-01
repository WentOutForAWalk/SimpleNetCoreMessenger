using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTO.Service;

public record ServiceResult
{
    [Required] public bool IsSuccess {  get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
