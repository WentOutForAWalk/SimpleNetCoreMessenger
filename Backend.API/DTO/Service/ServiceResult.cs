using System.ComponentModel.DataAnnotations;

namespace Backend.API.DTO.Service;

public record ServiceResult([Required] bool IsSuccess, string ErrorMessage = "")
{
    public static ServiceResult Success() => new ServiceResult(true);
    public static ServiceResult Failure(string message) => new ServiceResult(false, message);
}
