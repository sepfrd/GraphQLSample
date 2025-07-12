namespace Infrastructure.Common.Configurations;

public class AppOptions
{
    public required JwtOptions JwtOptions { get; set; }

    public required GraphQLOptions GraphQLOptions { get; set; }

    public required MongoDbOptions MongoDbOptions { get; set; }

    public required DataSeedOptions DataSeedOptions { get; set; }

    public required string ApplicationUrl { get; set; }
}