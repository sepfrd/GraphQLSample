namespace Domain.Abstractions;

public class DomainResult<T> : DomainResult where T : class
{
    protected DomainResult(bool isSuccess, Error error, T? data) : base(isSuccess, error)
    {
        Data = data;
    }

    public T? Data { get; set; }

    public static DomainResult<T> Success(T data) => new(true, Error.None, data);

    public static new DomainResult<T> Failure(Error error) => new(false, error, null);
}

public class DomainResult
{
    protected DomainResult(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static DomainResult Success() => new(true, Error.None);

    public static DomainResult Failure(Error error) => new(false, error);
}