namespace Application.Common;

public static class Extensions
{
    public static List<T> Paginate<T>(this IEnumerable<T> items, Pagination pagination)
    {
        var skipNumber = (pagination.Page - 1) * pagination.PageSize;

        return items
            .Skip(skipNumber)
            .Take(pagination.PageSize)
            .ToList();
    }
}