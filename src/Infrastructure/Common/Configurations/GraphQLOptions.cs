namespace Infrastructure.Common.Configurations;

public class GraphQLOptions
{
    public int MaxPageSize { get; set; }

    public double ExecutionTimeoutSeconds { get; set; }

    public double MaxFieldCost { get; set; }

    public double MaxTypeCost { get; set; }

    public double DefaultResolverCost { get; set; }

    public bool IncludeTotalCount { get; set; }

    public bool IncludeExceptionDetails { get; set; }

    public bool EnforceCostLimits { get; set; }

    public bool ApplyCostDefaults { get; set; }
}