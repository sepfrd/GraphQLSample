namespace Domain.Abstractions;

public abstract class DomainEntity : IEntity<Guid>, IAuditable
{
    protected DomainEntity()
    {
        CreatedAt = UpdatedAt = DateTime.UtcNow;
        Id = Guid.CreateVersion7();
    }

    public Guid Id { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; set; }

    public void MarkAsUpdated() => UpdatedAt = DateTimeOffset.UtcNow;
}