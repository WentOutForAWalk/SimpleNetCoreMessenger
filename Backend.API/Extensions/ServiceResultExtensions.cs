using Backend.API.DTO.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Extensions;
public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult<T>(this ServiceDataResult<T> result)
    {
        if (result.IsSuccess && result.Data is { }) return new OkObjectResult(result.Data);
        if (result.IsSuccess) return new OkResult();
        return new BadRequestObjectResult(result.ErrorMessage);
    }
    public static IActionResult ToActionResult(this ServiceResult result)
    {
        if (result.IsSuccess) return new OkResult();
        return new BadRequestObjectResult(result.ErrorMessage);
    }
}

