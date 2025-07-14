namespace Domain.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = UpdatedAt = DateTime.UtcNow;
        Uuid = Guid.CreateVersion7();
    }

    public Guid Uuid { get; init; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}