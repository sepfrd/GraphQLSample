namespace Domain.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        DateCreated = DateModified = DateTime.UtcNow;
        InternalId = Guid.NewGuid();
    }

    public Guid InternalId { get; set; }

    public required int ExternalId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}