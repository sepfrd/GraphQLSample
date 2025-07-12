namespace Infrastructure.Common.Configurations;

public class GraphQLOptions
{
    public required int MaxPageSize { get; set; }

    public required double ExecutionTimeoutSeconds { get; set; }

    public required double MaxFieldCost { get; set; }

    public required double MaxTypeCost { get; set; }

    public required double DefaultResolverCost { get; set; }

    public required bool IncludeTotalCount { get; set; }

    public required bool IncludeExceptionDetails { get; set; }

    public required bool EnforceCostLimits { get; set; }

    public required bool ApplyCostDefaults { get; set; }
}