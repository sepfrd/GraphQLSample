namespace Infrastructure.Common.Configurations;

public record MongoDbOptions
{
    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }
}