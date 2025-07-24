using Domain.ValueObjects;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class SkillFilterType : FilterInputType<Skill>
{
    protected override void Configure(IFilterInputTypeDescriptor<Skill> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .Field(skill => skill.Name)
            .Type<CustomStringOperationFilterType>();
    }
}