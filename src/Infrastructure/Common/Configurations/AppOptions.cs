namespace Infrastructure.Common.Configurations;

public class AppOptions
{
    public JwtOptions? JwtOptions { get; set; }

    public GraphQLOptions? GraphQLOptions { get; set; }

    public MongoDbOptions? MongoDbOptions { get; set; }

    public bool EnableDataSeed { get; set; }

    public string? ApplicationBaseAddress { get; set; }

    public string? ServerUrl { get; set; }

    public string? ClientUrl { get; set; }
}