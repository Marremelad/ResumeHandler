namespace ResumeHandler.Common;

public class Response<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string? Error { get; }

    private Response(bool isSuccess, T value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Response<T> Success(T value) => new Response<T>(true, value, null);
    public static Response<T?> Failure(string error) => new Response<T?>(false, default, error);
}