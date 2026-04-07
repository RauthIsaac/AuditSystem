namespace YourProject.Application.Wrappers;

public class RequestResponse<T>
{
    /*------------------------------------------------------------------*/
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    /*------------------------------------------------------------------*/
    public RequestResponse() { }
    /*------------------------------------------------------------------*/
    public RequestResponse(T data, string message, bool isSuccess)
    {
        Data = data;
        Message = message;
        IsSuccess = isSuccess;
    }
    /*------------------------------------------------------------------*/
    public static RequestResponse<T> Success(T data, string message = "Operation completed successfully")
        => new(data, message, true);
    /*------------------------------------------------------------------*/
    public static RequestResponse<T> Fail(string message = "Operation failed")
        => new(default!, message, false);
    /*------------------------------------------------------------------*/
}