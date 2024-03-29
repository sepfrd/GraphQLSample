namespace Domain.Common;

public sealed record Pagination(int PageNumber = 1, int PageSize = 10)
{
    public static Pagination MaxPagination => new(1, int.MaxValue);
}