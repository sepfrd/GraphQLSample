using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class QuestionSortType : SortInputType<Question>
{
    protected override void Configure(ISortInputTypeDescriptor<Question> descriptor)
    {
        descriptor
            .Field(question => question.InternalId)
            .Ignore();

        descriptor
            .Field(question => question.UserId)
            .Ignore();

        descriptor
            .Field(question => question.ProductId)
            .Ignore();
    }
}