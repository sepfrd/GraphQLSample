using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class PersonSortType : SortInputType<Person>
{
    protected override void Configure(ISortInputTypeDescriptor<Person> descriptor)
    {
        descriptor
            .Field(person => person.InternalId)
            .Ignore();

        descriptor
            .Field(person => person.UserId)
            .Ignore();
    }
}