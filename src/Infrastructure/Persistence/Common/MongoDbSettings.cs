namespace Infrastructure.Persistence.Common;

public record MongoDbSettings
(
    string ConnectionString,
    string DatabaseName,
    string CollectionName
);