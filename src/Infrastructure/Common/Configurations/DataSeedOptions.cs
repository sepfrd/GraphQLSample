namespace Infrastructure.Common.Configurations;

public class DataSeedOptions
{
    public bool ShouldSeed { get; set; }

    public int ItemsCount { get; set; }

    public int ItemsLargeCount { get; set; }

    public required string AdminUsername { get; set; }

    public required string AdminPassword { get; set; }
    public required string AdminEmail { get; set; }

    public required string UserUsername { get; set; }

    public required string UserPassword { get; set; }
    public required string UserEmail { get; set; }
}