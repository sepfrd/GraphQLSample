namespace Domain.Filters;

public record CustomUserFilter(int? ExternalId, string? Email, string? Username) : BaseFilter(ExternalId);