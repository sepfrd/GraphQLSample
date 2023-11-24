namespace Domain.Common;

public sealed record Pagination(int PageNumber = 1, int PageSize = 10);