namespace Domain.Filters;

public record CustomProductFilter(int? ExternalId, string? Name, string? Description, decimal? Price) : BaseFilter(ExternalId);