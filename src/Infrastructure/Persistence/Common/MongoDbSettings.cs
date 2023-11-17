namespace Infrastructure.Persistence.Common;

public record MongoDbSettings
{
    public required string ConnectionString { get; set; }

    public required string DatabaseName { get; set; }
}