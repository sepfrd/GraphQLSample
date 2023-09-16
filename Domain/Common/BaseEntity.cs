namespace Domain.Common;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        DateCreated = DateModified = DateTime.UtcNow;
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}