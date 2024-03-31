using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class CommentSortType : SortInputType<Comment>
{
    protected override void Configure(ISortInputTypeDescriptor<Comment> descriptor)
    {
        descriptor
            .Field(comment => comment.InternalId)
            .Ignore();

        descriptor
            .Field(comment => comment.UserId)
            .Ignore();

        descriptor
            .Field(comment => comment.ProductId)
            .Ignore();
    }
}