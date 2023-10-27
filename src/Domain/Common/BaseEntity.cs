using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        DateCreated = DateModified = DateTime.UtcNow;
        InternalId = Guid.NewGuid();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid InternalId { get; init; }

    public required int ExternalId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}