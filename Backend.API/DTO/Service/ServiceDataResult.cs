namespace Backend.API.DTO.Service;

public record ServiceDataResult<T>(bool IsSuccess, T? Data = default, string ErrorMessage = "") : ServiceResult(IsSuccess, ErrorMessage)
{
    public static ServiceDataResult<T> SuccessWithDate(T data) => new ServiceDataResult<T>(true, data);

    public static new ServiceDataResult<T> Success() => new ServiceDataResult<T>(true);
    public static new ServiceDataResult<T> Failure(string message) => new ServiceDataResult<T>(false, default, message);
}
