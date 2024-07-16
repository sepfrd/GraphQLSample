using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class PaymentSortType : SortInputType<Payment>
{
    protected override void Configure(ISortInputTypeDescriptor<Payment> descriptor)
    {
        descriptor
            .Field(payment => payment.InternalId)
            .Ignore();

        descriptor
            .Field(payment => payment.OrderId)
            .Ignore();
    }
}