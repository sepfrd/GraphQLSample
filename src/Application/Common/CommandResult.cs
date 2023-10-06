namespace Application.Common;

public sealed record CommandResult
{
    private CommandResult(bool isSuccessful, string message)
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }

    public bool IsSuccessful { get; }

    public string Message { get; } = string.Empty;

    public static CommandResult Success(string message) =>
        new CommandResult(true, message);

    public static CommandResult Failure(string message) =>
        new CommandResult(false, message);
}