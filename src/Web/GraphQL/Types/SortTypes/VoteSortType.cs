using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class VoteSortType : SortInputType<Vote>
{
    protected override void Configure(ISortInputTypeDescriptor<Vote> descriptor)
    {
        descriptor
            .Field(vote => vote.Content)
            .Ignore();

        descriptor
            .Field(vote => vote.InternalId)
            .Ignore();

        descriptor
            .Field(vote => vote.UserId)
            .Ignore();

        descriptor
            .Field(vote => vote.ContentId)
            .Ignore();
    }
}