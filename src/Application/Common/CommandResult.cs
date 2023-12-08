namespace Application.Common;

public sealed record CommandResult
{
    private CommandResult(bool isSuccessful, string message, string? data = null)
    {
        IsSuccessful = isSuccessful;
        Message = message;
        Data = data;
    }

    public string? Data { get; }

    public bool IsSuccessful { get; }

    public string Message { get; } = string.Empty;

    public static CommandResult Success(string message, string? data = null) =>
        new(true, message, data);

    public static CommandResult Failure(string message) =>
        new(false, message);
}