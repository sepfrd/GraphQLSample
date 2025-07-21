namespace Domain.Abstractions;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; init; }

    DateTimeOffset UpdatedAt { get; set; }

    void MarkAsUpdated();
}