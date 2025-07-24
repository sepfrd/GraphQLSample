namespace Domain.Abstractions;

public class DomainResult<T> : DomainResult where T : class
{
    protected DomainResult(bool isSuccess, Error error, T? data, int statusCode)
        : base(isSuccess, error, statusCode)
    {
        Data = data;
    }

    public T? Data { get; set; }

    public static DomainResult<T> Success(T data, int statusCode = 200) => new(true, Error.None, data, statusCode);

    public static new DomainResult<T> Failure(Error error, int statusCode) => new(false, error, null, statusCode);
}

public class DomainResult
{
    protected DomainResult(bool isSuccess, Error error, int statusCode)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public int StatusCode { get; }

    public static DomainResult Success(int statusCode = 200) => new(true, Error.None, statusCode);

    public static DomainResult Failure(Error error, int statusCode) => new(false, error, statusCode);
}