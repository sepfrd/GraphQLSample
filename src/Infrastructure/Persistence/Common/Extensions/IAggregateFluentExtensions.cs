using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Common.Extensions;

public static class AggregateFluentExtensions
{
    // TODO: Get object class name
    public static IAggregateFluent<T> IncludeRelations<T>(this IAggregateFluent<T> aggregateFluent, IEnumerable<Expression<Func<T, object?>>> includes)
    {
        foreach (var include in includes)
        {
            var foreignCollectionName = include.ReturnType + "s";
            var localFieldName = include.ReturnType + "Id";
            var foreignFieldName = include.ReturnType.ToString();

            aggregateFluent.Lookup(foreignCollectionName, localFieldName, "_id", foreignFieldName);
        }

        return aggregateFluent;
    }
}