namespace Domain.Abstractions;

public abstract class DomainEntity : IAuditable, IHasUuid
{
    protected DomainEntity()
    {
        CreatedAt = UpdatedAt = DateTime.UtcNow;
        Uuid = Guid.CreateVersion7();
    }

    public Guid Uuid { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; set; }

    public void MarkAsUpdated() => UpdatedAt = DateTimeOffset.UtcNow;
}