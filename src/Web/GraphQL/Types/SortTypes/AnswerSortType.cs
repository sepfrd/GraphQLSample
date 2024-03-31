using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class AnswerSortType : SortInputType<Answer>
{
    protected override void Configure(ISortInputTypeDescriptor<Answer> descriptor)
    {
        descriptor
            .Field(answer => answer.InternalId)
            .Ignore();

        descriptor
            .Field(answer => answer.UserId)
            .Ignore();

        descriptor
            .Field(answer => answer.QuestionId)
            .Ignore();
    }
}