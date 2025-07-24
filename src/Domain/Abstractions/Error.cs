namespace Domain.Abstractions;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
}